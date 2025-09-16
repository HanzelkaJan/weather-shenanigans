using System.Net;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace WeatherShenanigans;

public class WeatherChecker
{
    public string Source { get; set; }
    private bool firstTime = true;

    public WeatherChecker(string source)
    {
        this.Source = source;
    }

    public async Task CheckWeatherPeriodically(int minutes, CancellationToken token)
    {
        using PeriodicTimer timer = new(TimeSpan.FromMinutes(minutes));
        while (await timer.WaitForNextTickAsync() && !token.IsCancellationRequested)
        {
            CheckWeather();
        }
    }

    public void CheckWeather()
    {
        using (var context = new MyDbContext())
        {
            WeatherObservation entry = GetObservation();

            context.WeatherObservation.Add(entry);
            context.SaveChanges();
        }
    }
/*
    public List<WeatherObservation> GetEntries()
    {
        List<WeatherObservation> allEntries;
        using (var context = new MyDbContext())
        {
            allEntries = context.DbEntries.ToList();
        }
        return allEntries;
    }
*/

    public WeatherObservation GetObservation()
    {
        WeatherObservation observation = new WeatherObservation();
        try
        {
            WebRequest request = WebRequest.Create(Source);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            XmlSerializer serializer = new XmlSerializer(typeof(Wario));
            Wario wario = new Wario();
            wario = (Wario)serializer.Deserialize(reader);
            response.Close();
            firstTime = false;
            observation.Json = JsonSerializer.Serialize(wario);
        }
        catch (Exception e)
        {
            //Console.WriteLine(DateTime.Now + " --- " + e.Message);
            if (firstTime)
            {
                return FirstTimeCheck();
            }
            else
            {
                observation.Error = e.Message;
            }
        }
        observation.DlTime = DateTime.Now;
        return observation;
    }
    
    //Checks whether or not the program launched with the correct address as to not fill the database with failed attempts
    private WeatherObservation FirstTimeCheck()
    {
        while (true)
        {
            Console.WriteLine("Is the source address correct?(y/n): {0}", Source);
            string answer = Console.ReadLine();
            if (answer.Contains('n'))
            {
                Console.WriteLine("Please specify a new address:");
                Source = Console.ReadLine();
                return GetObservation();
            }

            if (answer.Contains('y'))
            {
                firstTime = false;
                return GetObservation();
            }
        }
    }
}