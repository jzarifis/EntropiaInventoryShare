using EntropiaInventoryShareWeb.Db;
using EntropiaInventoryShareWeb.Dto;
using EntropiaInventoryShareWeb.Entities;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using static MudBlazor.CategoryTypes;
using static MudBlazor.Colors;
using Avatar = EntropiaInventoryShareWeb.Entities.Avatar;
using Item = EntropiaInventoryShareWeb.Entities.Item;

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

                    //await WaitForPostgres();

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

        public async Task<string?> GetAvatarFromLicense(string license)
        {
            if (string.IsNullOrEmpty(license))
            {
                return string.Empty;
            }
            if (!Guid.TryParse(license, out var guid))
            {
                return string.Empty;
            }
            using (var scope = services.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                    return await dbContext.Avatars.Where(u => u.License == guid).Select(u => u.AvatarName).FirstOrDefaultAsync();
                    
                }
                catch (Exception ex)
                {
                    logger.LogError($"{ex}");
                    return string.Empty;
                }


            }
        }

        public async Task HandleSharedItemsAsync(List<InventoryItemDto> items, string avatar)
        {
            using (var scope = services.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    var dbAvatar = await GetAvatar(avatar);

                    var dbItems = await dbContext.SharedItems.Include(u => u.Item).Where(u => u.AvatarId == dbAvatar.Id).ToListAsync();
                    foreach (var dbItem in dbItems)
                    {
                        if (string.IsNullOrEmpty(dbItem.Item.Type))
                        {
                            await FetchFromEntropiaNexus(dbItem.Item);
                        }

                        var item = items.FirstOrDefault(u => !u.Shared && u.InShop == u.InShop && u.Name == dbItem.Item.Name && u.Value == dbItem.Value);
                        if (item == null)
                        {
                            item = items.FirstOrDefault(u => !u.Shared && u.InShop == u.InShop && u.Name == dbItem.Item.Name && u.Value == dbItem.Value);
                        }
                        if (item == null)
                        {
                            dbItem.Quantity = 0;
                            dbItem.Value = 0;
                        }
                        else
                        {
                            dbItem.Container = item.Container;
                            dbItem.Quantity = item.Quantity;
                            dbItem.Value = item.Value;
                            dbItem.Timestamp = DateTimeOffset.UtcNow;
                            item.MarkupPercentage = dbItem.MarkupPercentage;
                            item.MarkupAddToTT = dbItem.MarkupAddToTT;
                            item.PerItemPrice = dbItem.PerItemPrice;
                            item.Shared = true;
                            item.dbId = dbItem.Id;
                        }

                    }


                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    logger.LogError($"{ex}");

                }


            }
        }


        private async Task<Avatar> GetAvatar(string avatar)
        {
            using (var scope = services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                var dbAvatar = await dbContext.Avatars.SingleOrDefaultAsync(u => u.AvatarName == avatar);
                if (dbAvatar == null)
                {
                    dbAvatar = new Avatar
                    {
                        AvatarName = avatar,
                        License = Guid.NewGuid()
                    };
                    await dbContext.Avatars.AddAsync(dbAvatar);
                    await dbContext.SaveChangesAsync();
                }
                return dbAvatar;
            }
        }


        public async Task HandleItemSharedValueChangedAsync(InventoryItemDto item, string avatar)
        {
            using (var scope = services.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    var dbAvatar = await GetAvatar(avatar);


                    var sharedItem = await dbContext.SharedItems.SingleOrDefaultAsync(u => u.Id == item.dbId);
                    if (sharedItem != null)
                    {
                        sharedItem.PerItemPrice = item.PerItemPrice;
                        sharedItem.MarkupAddToTT = item.MarkupAddToTT;
                        sharedItem.MarkupPercentage = item.MarkupPercentage;
                        sharedItem.Timestamp = DateTimeOffset.UtcNow;
                    }

                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    logger.LogError($"{ex}");

                }


            }
        }


        public async Task HandleItemSharedStateAsync(InventoryItemDto item, string avatar)
        {
            using (var scope = services.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                    var dbAvatar = await GetAvatar(avatar);

                    var dbItem = await dbContext.Items.SingleOrDefaultAsync(u => u.Name == item.Name);
                    if (dbItem == null)
                    {
                        dbItem = new Item
                        {
                            Name = item.Name
                        };
                        await dbContext.Items.AddAsync(dbItem);
                        await dbContext.SaveChangesAsync();
                    }

                    if (string.IsNullOrEmpty(dbItem.Type))
                    {
                        await FetchFromEntropiaNexus(dbItem);
                    }
                    InventorySharedItem? sharedItem;
                    if (!item.dbId.HasValue)
                    {
                        sharedItem = new InventorySharedItem
                        {
                            AvatarId = dbAvatar.Id,
                            InShop = item.InShop,

                            ItemId = dbItem.Id
                        };
                        await dbContext.SharedItems.AddAsync(sharedItem);
                    }
                    else
                    {
                        sharedItem = await dbContext.SharedItems.SingleOrDefaultAsync(u => u.Id == item.dbId);

                    }
                    sharedItem!.Container = item.Container;
                    sharedItem!.Quantity = item.Quantity;
                    sharedItem!.Value = item.Value;
                    sharedItem!.Timestamp = DateTimeOffset.UtcNow;

                    if (!item.Shared)
                    {
                        sharedItem.Quantity = 0;
                        sharedItem.Value = 0;
                    }

                    await dbContext.SaveChangesAsync();
                    if (item.Shared)
                    {
                        item.dbId = sharedItem.Id;
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"{ex}");

                }


            }
        }

        public async Task FetchFromEntropiaNexus(Item item)
        {
            try
            {

                using (var scope = services.CreateScope())
                {
                    try
                    {
                        var entropiaNexusService = scope.ServiceProvider.GetService<EntropiaNexusService>();
                        var entropiaNexusItem = await entropiaNexusService.GetGenericItemInfo(item.Name);
                        if (entropiaNexusService != null)
                        {
                            item.Weight = entropiaNexusItem.Properties?.Weight;
                            item.Value = entropiaNexusItem.Properties.Economy.Value;
                            item.Type = entropiaNexusItem.Properties.Type;
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.LogError($"{ex}");

                    }


                }


            }
            catch (Exception ex)
            {
                logger.LogError($"{ex}");
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
