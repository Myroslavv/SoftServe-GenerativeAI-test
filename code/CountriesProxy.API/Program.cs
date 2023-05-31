using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CountriesProxy.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        services.AddControllers();
                        services.AddHttpClient();
                    })
                    .Configure((hostContext, app) =>
                    {
                        if (hostContext.HostingEnvironment.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }

                        app.UseHttpsRedirection();
                        app.UseRouting();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}
