using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using WeatherForecast.GrpcService;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel =GrpcChannel.ForAddress("https://localhost:5001");
            var client = new WeatherService.WeatherServiceClient(channel);
            var request  = new WeatherForecastRequest
            {
                CityName = "Berlin",
                FromDate = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
                ToDate = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime().AddDays(5))
            };
            var result = await client.GetWeatherForecastAsync(request);
            foreach (var item in result.Items)
            {
                Console.WriteLine($"Weather for {item.CityName} in {item.Date} is {item.Summary} {item.TemperatureC}°C/{ item.TemperatureK}K");
            }

            Console.ReadLine();
        }
    }
}
