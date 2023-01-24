using BrewCoffee.RestAdapter.Middleware;

namespace BrewCoffee.RestAdapter.Startup
{
    public static class ApplicationSetup
    {
        public static WebApplication Configure(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
