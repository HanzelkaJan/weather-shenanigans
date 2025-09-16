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

    public async void CheckWeatherPeriodically(int minutes)
    {
        using PeriodicTimer timer = new(TimeSpan.FromMinutes(minutes));
        while (await timer.WaitForNextTickAsync())
        {
            CheckWeather();
        }
    }

    public void CheckWeather()
    {
        using (var context = new MyDbContext())
        {
            WeatherObservation entry = GetObservation();
            try
            {
                entry.Id = context.DbEntries.Max(p => p.Id)+1;
            }
            catch (Exception e)
            {
                entry.Id = 1;
            }
            context.DbEntries.Add(entry);
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
            observation.Json = ConvertToJson(wario);
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

    public string ConvertToJson(Wario input)
    {
        string json = JsonSerializer.Serialize(input);
        return json;
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
    public class MyDbContext : DbContext
    {
        public DbSet<WeatherObservation> DbEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("DummyDb");
        }
    }
    
}