using Microsoft.AspNetCore.Mvc;
using BrewCoffee.Application.Models;
using BrewCoffee.Application.Ports.In;

namespace BrewCoffee.Controllers
{
    [ApiController]
    [Route("brew-coffee")]
    public class BrewCoffeeController : ControllerBase
    {
        IGetBrewedCoffeeService _getCoffeeService;
        public BrewCoffeeController(IGetBrewedCoffeeService getCoffeeService)
        {
            _getCoffeeService = getCoffeeService;
        }

        [HttpGet]
        public Response GetCoffee()
        {
            var response = _getCoffeeService.GetBrewedCoffee();

            return response;
        }
    }
}