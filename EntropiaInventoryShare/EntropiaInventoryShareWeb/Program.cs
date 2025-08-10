using EntropiaInventoryShareWeb.Components;
using EntropiaInventoryShareWeb.Db;
using EntropiaInventoryShareWeb.Entities;
using EntropiaInventoryShareWeb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using System;
using System.Security.Claims;
using System.Xml.Linq;

namespace EntropiaInventoryShareWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var keysPath = Environment.GetEnvironmentVariable("DP_KEYS_PATH")
               ?? "/app/dpkeys"; // must be a mounted volume

            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(keysPath))
                .SetApplicationName("EIS"); 


            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

            builder.Services.AddSingleton<MainBackgroundService>();

            _ = builder.Services.AddHostedService<MainBackgroundService>(provider => provider.GetService<MainBackgroundService>()!);

            builder.Services.AddHttpClient();

            builder.Services.AddScoped<EntropiaNexusService>();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddMudServices();

            builder.Services.AddScoped<ParsingService>();


            builder.Services.AddHttpContextAccessor();


            builder.Services
                .AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "app.auth";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.ExpireTimeSpan = TimeSpan.FromDays(90);
                    options.SlidingExpiration = false;
                });

            builder.Services.ConfigureApplicationCookie(
                options =>
                {
                    options.Events.OnRedirectToAccessDenied =
                        options.Events.OnRedirectToLogin =
                            context =>
                            {
                                context.Response.StatusCode = 401;
                                return Task.CompletedTask;
                            };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAntiforgery();

            app.MapStaticAssets();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();


            app.MapGet("/logout", async (HttpContext ctx) =>
            {
                await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Redirect("/");
            });

            app.MapGet("/login", async (
                string t,
                HttpContext ctx,
                AppDbContext dbContext,
                [FromServices] IDataProtectionProvider dataProtectionProvider) =>
            {

                var protector = dataProtectionProvider.CreateProtector("SignIn");

                var license = protector.Unprotect(t);
                var avatar = await dbContext.Avatars.SingleOrDefaultAsync(u => u.License == Guid.Parse(license));
   

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, avatar.AvatarName),
                    new Claim(ClaimTypes.Name, avatar.AvatarName),
                    new Claim("auth_type", "key")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = avatar.LicenseExpiration,
                    AllowRefresh = true
                };

                await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

                return Results.Redirect("/");
            });




            app.Run();
        }
    }
}
