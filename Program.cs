namespace WeatherShenanigans;

class Program
{
    static void Main(string[] args)
    {
        //https://pastebin.com/raw/PMQueqDV
        WeatherChecker checker = new WeatherChecker(args[0]);
        
        checker.CheckWeather();
        checker.CheckWeatherPeriodically(1);
        while (true)
        {
        }
    }
}