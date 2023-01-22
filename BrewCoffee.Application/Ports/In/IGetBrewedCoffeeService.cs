using BrewCoffee.Application.Models;

namespace BrewCoffee.Application.Ports.In
{
    public interface IGetBrewedCoffeeService
    {
        public Response GetBrewedCoffee();
    }
}
