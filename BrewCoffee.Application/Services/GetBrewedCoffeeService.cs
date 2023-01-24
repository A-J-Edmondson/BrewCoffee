using BrewCoffee.Application.Exceptions;
using BrewCoffee.Application.Extensions;
using BrewCoffee.Application.Interfaces;
using BrewCoffee.Application.Models;
using BrewCoffee.Application.Ports.In;
using Newtonsoft.Json;

namespace BrewCoffee.Application.Services
{
    public class GetBrewedCoffeeService : IGetBrewedCoffeeService
    {
        private string _coffeeMachineFilePath = null!;
        private IDateExtensions? _dateExtensions;

        public GetBrewedCoffeeService(IDateExtensions? dateExtensions = null)
        {
            _dateExtensions = dateExtensions;
        }

        public Response GetBrewedCoffee()
        {
            var coffeeMachine = GetCoffeeMachine();
            coffeeMachine.IncrementUseCount();
            SaveChanges(coffeeMachine);

            ThrowExceptionIfCoffeeMachineIsOutOfService();
            ThrowExceptionOn5thCoffeeMachineUse(coffeeMachine);
            
            var response = GetCoffeeMachineResponse();
            return response;
        }

        private static Response GetCoffeeMachineResponse()
        {
            return new Response()
            {
                Message = "Your piping hot coffee is ready",
                Prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
            };
        }

        private void ThrowExceptionOn5thCoffeeMachineUse(CoffeeMachine coffeeMachine)
        {
            if (coffeeMachine.UseCount % 5 == 0)
            {
                throw new OutOfCoffeeException();
            }
        }

        private void ThrowExceptionIfCoffeeMachineIsOutOfService()
        {
            if (_dateExtensions is null)
            {
                var dateExtensions = new DateExtensions();
                if (dateExtensions.GetCurrentDateInMonthsAndDays() == "04-01") // April fools day.
                {
                    throw new OutOfServiceException();
                }
            }
            else
            {
                if (_dateExtensions.GetCurrentDateInMonthsAndDays() == "04-01") // April fools day.
                {
                    throw new OutOfServiceException();
                }
            }
        }

        private CoffeeMachine GetCoffeeMachine()
        {
            // Get filepath of CoffeeMachine.json
            var solutionDirectory = DirectoryExtensions.GetSolutionDirectory();
            _coffeeMachineFilePath = Path.Combine(solutionDirectory, "BrewCoffee.Application\\Cache\\CoffeeMachine.json");

            // Fetch data from CoffeeMachine.json
            string? coffeeMachineData;
            try
            {
                coffeeMachineData = File.ReadAllText(_coffeeMachineFilePath);
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
                File.WriteAllText(_coffeeMachineFilePath, jsonCoffeeMachine);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
