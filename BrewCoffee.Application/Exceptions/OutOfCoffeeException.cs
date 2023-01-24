namespace BrewCoffee.Application.Exceptions
{
    public class OutOfServiceException : Exception
    {
        public OutOfServiceException(string error = "The Coffee Machine is out of service today.") : base(error)
        { }
    }
}
