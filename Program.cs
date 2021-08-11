using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using file_ingest_db.Extensions;
using file_ingest_db.Repositories;
using file_ingest_db.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace file_ingest_db
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new LogService();
            
            try
            {
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var config = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                    .Build();

                    log.Log($"Enviroment: {environmentName}");
                    log.Log($"ConnectionString: {config.GetConnectionString("ingestDb")}");
                    log.Log($"appSettings exists: {File.Exists("appsettings.json")}");
                    log.Log($"appSettings Docker exists: {File.Exists("appsettings.Docker.json")}");

                var servicesProvider = BuildDi(config);
                using (servicesProvider as IDisposable)
                {
                    var runner = servicesProvider.GetRequiredService<Runner>();
                    runner.DoAction();

                    log.Log("Done! Turning Off.");
                }
            }
            catch (Exception ex)
            {
                log.Log(ex.GetExceptionMessages());
            }
            finally
            {
            }

            log.Log("Shutdown");
        }

        private static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
                .AddTransient<Runner>()
                .AddDbContext<dbContext>(
                    options => options.UseSqlServer(config.GetConnectionString("ingestDb"))
                )
                .AddSingleton<IConfiguration>(config)
                .AddScoped<ITigerRepository, TigerRepository>()
                .AddScoped<ISnakeRepository, SnakeRepository>()
                .AddSingleton<ITigerService, TigerService>()
                .AddSingleton<ISnakeService, SnakeService>()
                .AddSingleton<ILogService, LogService>()
                .BuildServiceProvider();
        }
    }
}
