using Company.DAL.Context;
using Company.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CompanyMvc.Utilities
{
    public  class SeedingData
    {
        public static async Task Seeding(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var service= scope.ServiceProvider;
                var UserManager = service.GetRequiredService<UserManager<AppUser>>();
                var loggerFactory= service.GetRequiredService<ILoggerFactory>();
                var context = service.GetRequiredService<DataContext>();
                try
                {
                    await context.Database.MigrateAsync();
                    var user = await UserManager.FindByEmailAsync("Omar@gmail.com");
                    if (user == null)
                    {
                        var adminUser = new AppUser { Email = "Omar@gmail.com", FName = "Omar", LName = "ElQady", Agree = true, UserName = "OmarElQady" };
                        await UserManager.CreateAsync(adminUser, "Omar@123");
                        await UserManager.AddToRoleAsync(adminUser, "Admin");
                    }

                }
                catch (Exception ex)
                {
                    var logger=loggerFactory.CreateLogger<SeedingData>();
                    logger.LogError(ex.Message);
                }

            }

        }
    }
}
