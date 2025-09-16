namespace WeatherShenanigans;

class Program
{
    static async Task Main(string[] args)
    {
        //https://pastebin.com/raw/PMQueqDV
        WeatherChecker checker = new WeatherChecker(args[0]);

        var tokenSource = new CancellationTokenSource();
        var token = tokenSource.Token;
        
        checker.CheckWeather();
        Console.CancelKeyPress += (_, ea) =>
        {
            Console.WriteLine("Shutting down WeatherChecker");
            tokenSource.Cancel();
            tokenSource.Dispose();
        };
        await checker.CheckWeatherPeriodically(60, token);
    }
}