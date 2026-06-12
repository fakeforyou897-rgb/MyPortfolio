using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models.ViewModels.Admin;
using System.Threading.Tasks;
using System.Linq;

namespace MyPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> EditRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var model = new EditRolesViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email ?? string.Empty,
                Roles = _roleManager.Roles.Select(r => new RoleSelection
                {
                    RoleName = r.Name ?? string.Empty,
                    Selected = _userManager.IsInRoleAsync(user, r.Name ?? string.Empty).Result
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRoles(EditRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            foreach (var role in model.Roles)
            {
                if (role.Selected && !await _userManager.IsInRoleAsync(user, role.RoleName))
                {
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                }
                else if (!role.Selected && await _userManager.IsInRoleAsync(user, role.RoleName))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
