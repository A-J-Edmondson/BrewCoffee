namespace BrewCoffee.Application.Models
{
    public class CoffeeMachine
    {
        public int UseCount { get; set; }

        public void IncrementUseCount()
        {
            UseCount++;
        }
    }
}
