using Microsoft.AspNetCore.Mvc;

namespace BrewCoffee.Controllers
{
    [ApiController]
    [Route("brew-coffee")]
    public class BrewCoffeeController : ControllerBase
    {

        private readonly ILogger<BrewCoffeeController> _logger;

        public BrewCoffeeController(ILogger<BrewCoffeeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}