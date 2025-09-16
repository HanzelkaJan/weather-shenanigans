using System.Net;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

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
            CheckWeather(token);
        }
    }

    public async void CheckWeather(CancellationToken token)
    {
        using (var context = new MyDbContext())
        {
            context.Database.EnsureCreated();
            WeatherObservation entry = await GetObservation(token);

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

    public async Task<WeatherObservation> GetObservation(CancellationToken token)
    {
        WeatherObservation observation = new WeatherObservation();
        try
        {
            HttpClient client = new();
            using Stream response = await client.GetStreamAsync(Source, token);
            XmlSerializer serializer = new XmlSerializer(typeof(Wario));
            Wario wario = new Wario();
            wario = (Wario)serializer.Deserialize(response);
            firstTime = false;
            observation.Json = JsonSerializer.Serialize(wario);
        }
        catch (Exception e)
        {
            //Console.WriteLine(DateTime.Now + " --- " + e.Message);
            if (firstTime)
            {
                return await FirstTimeCheck(token);
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
    private async Task<WeatherObservation> FirstTimeCheck(CancellationToken token)
    {
        while (true)
        {
            Console.WriteLine("Is the source address correct?(y/n): {0}", Source);
            string answer = Console.ReadLine();
            if (answer.Contains('n'))
            {
                Console.WriteLine("Please specify a new address:");
                Source = Console.ReadLine();
                return await GetObservation(token);
            }

            if (answer.Contains('y'))
            {
                firstTime = false;
                return await GetObservation(token);
            }
        }
    }
}