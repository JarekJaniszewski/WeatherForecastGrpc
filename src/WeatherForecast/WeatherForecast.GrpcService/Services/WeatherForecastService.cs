using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace WeatherForecast.GrpcService.Services
{
    public class WeatherForecastService : WeatherService.WeatherServiceBase
    {
        private readonly ILogger<WeatherForecastService> _logger;
        public WeatherForecastService(ILogger<WeatherForecastService> logger)
        {
            _logger = logger;
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public override Task<WeatherForecastResponse> GetWeatherForecast(WeatherForecastRequest request, ServerCallContext context)
        {

            //Get random weather for a specific region.
            var rng = new Random();
            var value = rng.Next(-20, 55);
            var weatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecastItem
            {
                CityName = request.CityName,
                Date = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime().AddDays(index)),
                TemperatureC = value,
                TemperatureK = 32 + (int)(value / 0.5556),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();

            //Create response 
            var result = new WeatherForecastResponse();
            foreach (var weather in weatherForecast)
            {
                result.Items.Add(weather);
            }

            return Task.FromResult(result);
        }
    }
}
