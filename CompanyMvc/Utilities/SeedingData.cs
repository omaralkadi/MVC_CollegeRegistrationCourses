using Company.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace CompanyMvc.Utilities
{
    public static class SeedingData
    {
        public static async Task Seeding(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var service= scope.ServiceProvider;
                var UserManager = service.GetRequiredService<UserManager<AppUser>>();
                
                var user = await UserManager.FindByEmailAsync("Omar@gmail.com");
                if (user == null)
                {
                    var adminUser = new AppUser {Email="Omar@gmail.com",FName="Omar",LName="ElQady",Agree=true,UserName="OmarElQady"};
                    await UserManager.CreateAsync(adminUser,"Omar@123");
                    await UserManager.AddToRoleAsync(adminUser,"Admin");
                }
            }

        }
    }
}
