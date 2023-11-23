using Microsoft.AspNetCore.Identity;
using VotieAPI.Auth.Entity;

namespace VotieAPI.Auth
{
    public class AuthDbSeeder
    {
        private readonly UserManager<VotieUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthDbSeeder(UserManager<VotieUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedAsync()
        {
            await AddDefaultRoles();
            await AddAdminUser();
        }

        private async Task AddDefaultRoles()
        {
            foreach (var role in VotieRoles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists) 
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        private async Task AddAdminUser()
        {
            var newAdminUser = new VotieUser
            {
                Email = "adminukas@adminmail.com",
                UserName = "adminukas",
                Name = "admin",
                LastName = "admin"
            };

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null)
            {
                var createAdmindUserResult = await _userManager.CreateAsync(newAdminUser, "Nepaimsi326.");
                if (createAdmindUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, VotieRoles.All);
                }
            }
        }
    }
}
