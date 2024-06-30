using Hangfire;
using Microsoft.AspNetCore.Mvc;
using MyHangFire.Services.IServices;

namespace MyHangFire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBackgroundJobClient _jobClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBackgroundJobClient jobClient)
        {
            _logger = logger;
            _jobClient = jobClient;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //fire and forget job
            var jobId = _jobClient.Enqueue<IEmailService>(x => x.SendWelcomeEmail("test@test.cz", "Test"));
            //Delayed job
            var jobScheduleId = _jobClient.Schedule<IEmailService>(x => x.SendGettingStartedEmail("test@test.cz", "Test"), TimeSpan.FromSeconds(5));

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
