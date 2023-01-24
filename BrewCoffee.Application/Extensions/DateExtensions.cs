namespace BrewCoffee.Application.Extensions
{
    public class DateExtensions
    {
        public string GetCurrentDateInMonthsAndDays()
        {
            return DateTime.Now.ToString("MM-dd");
        }
    }
}
