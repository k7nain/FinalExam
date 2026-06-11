using FinalExam.Models;
using FinalExam.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalExam.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again");
                return View(registerVM);
            }

            AppUser user = new AppUser
            {
                UserName = registerVM.Username,
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    return View(registerVM);
                }
            }

            await _userManager.AddToRoleAsync(user, "User");

            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again");
                return View(loginVM);
            }

            AppUser user = await _userManager.FindByEmailAsync(loginVM.Email);

            if (user is null)
            {
                ModelState.AddModelError("Email", "Email or password is valid");
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Email", "Email or password is valid");
                return View(loginVM);
            }

            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Manager"));

            return Content("Okey");
        }
    }
}
