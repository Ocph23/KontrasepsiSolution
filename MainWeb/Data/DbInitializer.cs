using Microsoft.AspNetCore.Identity;

namespace MainWeb.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            if (!context.Roles.Any())
            {
                try
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("Petugas"));
                    await roleManager.CreateAsync(new IdentityRole("Pasien"));
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }

            if (!context.Users.Any())
            {
                try
                {
                    var user = new IdentityUser { UserName = "ocph23.test@gmail.com", Email = "ocph23.test@gmail.com", EmailConfirmed = true };
                    var result = await userManager.CreateAsync(user, "Sony@77");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }

                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }

            }
        }



    }
}