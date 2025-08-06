using EntropiaInventoryShareWeb.Db;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;

namespace EntropiaInventoryShareWeb.Services
{
    public class MainBackgroundService(ILogger<MainBackgroundService> logger, IServiceProvider services, IConfiguration configuration) : IHostedService
    {


        public async Task StartAsync(CancellationToken cancellationToken)
        {

            using (var scope = services.CreateScope())
            {
                try
                {

                    logger.LogWarning($"Main Background Service is starting");

                    await WaitForPostgres();

                    logger.LogWarning($"Migrate Database Started.");


                    var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    dbContext!.Database.SetCommandTimeout(300);

                    await dbContext.Database.MigrateAsync();

                    logger.LogWarning($"Migrate Database Finished.");

                }
                catch (Exception ex)
                {
                    logger.LogError($"{ex}");

                    throw;
                }



            }
        }




        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }

        async Task WaitForPostgres()
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            int maxRetries = 200;
            int delayMilliseconds = 1000;

            for (int i = 1; i <= maxRetries; i++)
            {
                try
                {
                    logger.LogInformation($"Attempt {i} to connect to the database...");
                    var canConnect = await dbContext.Database.CanConnectAsync();
                    if (canConnect)
                    {
                        logger.LogWarning("Database is available.");
                        return; // Exit loop if successful
                    }
                    else
                    {
                        logger.LogWarning($"Database not ready (attempt {i}/{maxRetries})");
                        if (i == maxRetries)
                        {
                            logger.LogError("Database connection failed after multiple attempts.");
                            throw new Exception("Database connection failed after multiple attempts.");
                        }
                        await Task.Delay(delayMilliseconds);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Database not ready (attempt {i}/{maxRetries}): {ex.Message}");
                    if (i == maxRetries)
                    {
                        logger.LogError("Database connection failed after multiple attempts.");
                        throw;
                    }
                    await Task.Delay(delayMilliseconds);
                }
            }

        }


    }
}
