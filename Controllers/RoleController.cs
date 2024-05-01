using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechStore.Data.Entities;

namespace TechStore.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateRoles()
        {
            string[] roleNames = { "Admin", "User" };
            string[] roleDescs = { "Admin giving full rights.", "Standard user with limited rights." };

            for (int i = 0; i < roleNames.Length; i++)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleNames[i]);
                if (!roleExist)
                {
                    var roleResult = await _roleManager.CreateAsync(new ApplicationRole { Name = roleNames[i], Description = roleDescs[i] });
                }
            }

            return Ok("Roles are created.");
        }
    }
}
