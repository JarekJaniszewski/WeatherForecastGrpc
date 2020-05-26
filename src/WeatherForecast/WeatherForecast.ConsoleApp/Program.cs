using System;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using WeatherForecast.GrpcService;
using static WeatherForecast.GrpcService;

namespace WeatherForecast.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new WeatherService.WeatherServiceClient(channel);
            var reply = await client.GetWeatherForecastAsync(
                new WeatherForecastRequest 
                    { 
                        CityName = "Berlin",
                        FromDate = Timestamp.FromDateTime(DateTime.Now),
                        ToDate = Timestamp.FromDateTime(DateTime.Now.AddDays(5))
                    });

            Console.WriteLine("Hello World!");
        }
    }
}
