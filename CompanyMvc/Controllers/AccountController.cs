using Company.DAL.Entities;
using CompanyMvc.Utilities;
using CompanyMvc.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CompanyMvc.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        public SignInManager<AppUser> _signInManager { get; }

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                    PhoneNumber = registerVM.Phone,
                    UserName = registerVM.FName + registerVM.LName
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
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(loginVM.Email);
                if (User is not null)
                {
                    var UserName = await _signInManager.PasswordSignInAsync(User, loginVM.Password, loginVM.RememberMe, false);
                    if (UserName.Succeeded)
                        return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError("", "InCorrect Email Or Password ");
            }
            return View(loginVM);
        }

        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is not null)
            {
                var Token = await _userManager.GeneratePasswordResetTokenAsync(User);
                var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token = Token }, Request.Scheme);

                var email = new Email
                {
                    Subject = "Reset Password",
                    Body = url,
                    Recipient = model.Email
                };

                SendEmail.send(email);

                return RedirectToAction(nameof(CheckYourInBox));
            }

            ModelState.AddModelError("", "Email Does not Exist ");
            return View();
        }

        public async Task<IActionResult> CheckYourInBox()
        {
            return View();
        }

        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var email = TempData["email"] as string;
            var token = TempData["token"] as string;
            if (!ModelState.IsValid) { return View(model); }


            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                var result=await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(PasswordChangedSuccessfully));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ModelState.AddModelError("","Email Does not Exist");

            return View(model);
        }

        public async Task<IActionResult> PasswordChangedSuccessfully()
        {
            return View();
        }


    }
}