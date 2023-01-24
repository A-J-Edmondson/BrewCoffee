namespace BrewCoffee.Application.Exceptions
{
    public class OutOfCoffeeException : Exception
    {
        public OutOfCoffeeException(string error = "The Coffee Machine is out of Coffee.") : base(error)
        { }
    }
}
