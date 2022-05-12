using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.ViewModels;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]//Role/Craete "Get"
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]//Role/Craete "post"
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleVM newRole)
        {
            if (ModelState.IsValid == true)
            {
                IdentityRole role = new IdentityRole();
                role.Name = newRole.RoleName;
                IdentityResult result = await roleManager.CreateAsync(role);//name already exist
                if (result.Succeeded == true)
                {
                    return View(new RoleVM());
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                //save
            }
            return View(newRole);
        }

    }
}
