using BrewCoffee.Application.Ports.In;
using BrewCoffee.Application.Services;

namespace BrewCoffee.RestAdapter.Startup
{
    public static class DependencyInjectionSetup
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IGetBrewedCoffeeService, GetBrewedCoffeeService>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
