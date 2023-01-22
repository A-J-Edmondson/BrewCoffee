using BrewCoffee.Application.Models;
using BrewCoffee.Application.Ports.In;

namespace BrewCoffee.Application.Services
{
    public class GetBrewedCoffeeService : IGetBrewedCoffeeService
    {
        public Response GetBrewedCoffee()
        {
            var response = new Response()
            {
                Message = "Your piping hot coffee is ready",
                Prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
            };

            return response;
        }
    }
}
