using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Security.Claims;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager
            , SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM userVm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = userVm.UserName,
                    Email = userVm.Email,
                    Address = userVm.Address,
                    //PasswordHash = userVm.Password
                };
                IdentityResult result = await userManager.CreateAsync(newUser, userVm.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(newUser, false);
                    return RedirectToAction("Index", "Categories");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(userVm);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            LoginVM model = new LoginVM { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM userVm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(userVm.Email);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, userVm.Password);
                    if (found)
                    {
                        //List<Claim> claims = new List<Claim>();
                        //claims.Add(new Claim("Address", "Cairo"));
                        //await signInManager.SignInWithClaimsAsync(user, userVm.RememberMe, claims);
                        await signInManager.SignInAsync(user, userVm.RememberMe);


                        if (!string.IsNullOrEmpty(userVm.ReturnUrl) && Url.IsLocalUrl(userVm.ReturnUrl))
                        {
                            //return RedirectToAction("Index", "Home");
                            return Redirect(userVm.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Categories");
                        }
                    }
                }
                ModelState.AddModelError("", "Name & password Not Correct");
            }
            return View(userVm);
        }

        //[HttpPost]
        public async Task<IActionResult> Logout(string cart)
        {
            string id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            ApplicationUser user = await userManager.FindByIdAsync(id);

            //add claims to db
            await userManager.AddClaimAsync(user, new Claim("Cart", cart));
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(RegisterVM userVm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = userVm.UserName,
                    Email = userVm.Email,
                    Address = userVm.Address,
                    //PasswordHash = userVm.Password
                };
                IdentityResult result = await userManager.CreateAsync(newUser, userVm.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "Admin");
                    //await signInManager.SignInAsync(newUser, false);
                    return RedirectToAction("Index", "Categories");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(userVm);
        }

    }

}
