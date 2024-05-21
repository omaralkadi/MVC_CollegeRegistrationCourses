using Company.DAL.Entities;
using CompanyMvc.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                var User = new AppUser
                {
                    FName = registerVM.FName,
                    Email = registerVM.Email,
                    LName = registerVM.LName,
                    Agree = registerVM.Agree,
                    PhoneNumber=registerVM.Phone,
                    UserName=registerVM.FName+registerVM.LName
                };
                var result = await _userManager.CreateAsync(User, registerVM.Password);
                if (result.Succeeded)
                    return RedirectToAction("Login");
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }

            return View(registerVM);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            return View();
        }
    }
}
