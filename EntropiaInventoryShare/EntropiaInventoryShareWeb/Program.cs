using EntropiaInventoryShareWeb.Components;
using EntropiaInventoryShareWeb.Db;
using EntropiaInventoryShareWeb.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using System;

namespace EntropiaInventoryShareWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));


            builder.Services.AddHostedService<MainBackgroundService>();

            builder.Services.AddScoped<EntropiaNexusService>();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddMudServices();

            builder.Services.AddScoped<ParsingService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
