using BrewCoffee.Application.Exceptions;
using BrewCoffee.Application.Extensions;
using BrewCoffee.Application.Models;
using BrewCoffee.Application.Ports.In;
using Newtonsoft.Json;

namespace BrewCoffee.Application.Services
{
    public class GetBrewedCoffeeService : IGetBrewedCoffeeService
    {
        internal string CoffeeMachineFilePath { get; set; } = null!;
        public Response GetBrewedCoffee()
        {
            var coffeeMachine = GetCoffeeMachine();

            coffeeMachine.IncrementUseCount();

            SaveChanges(coffeeMachine);

            if (coffeeMachine.UseCount % 5 == 0)
            {
                throw new OutOfCoffeeException();
            }

            var response = new Response()
            {
                Message = "Your piping hot coffee is ready",
                Prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
            };

            return response;
        }

        private CoffeeMachine GetCoffeeMachine()
        {
            // Get filepath of CoffeeMachine.json
            var solutionDirectory = DirectoryExtensions.GetSolutionDirectory();
            CoffeeMachineFilePath = Path.Combine(solutionDirectory, "BrewCoffee.Application\\Cache\\CoffeeMachine.json");

            // Fetch data from CoffeeMachine.json
            string? coffeeMachineData;
            try
            {
                coffeeMachineData = File.ReadAllText(CoffeeMachineFilePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var coffeeMachine = JsonConvert.DeserializeObject<CoffeeMachine>(coffeeMachineData);
            return coffeeMachine!;
        }

        private void SaveChanges(CoffeeMachine coffeeMachine)
        {
            var jsonCoffeeMachine = JsonConvert.SerializeObject(coffeeMachine, Formatting.Indented);
            try
            {
                File.WriteAllText(CoffeeMachineFilePath, jsonCoffeeMachine);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
