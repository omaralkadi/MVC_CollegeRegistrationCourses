using AutoMapper;
using Company.DAL.Entities;
using CompanyMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMvc.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _UserManager;
        public IMapper _mapper { get; }
        public UserController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _UserManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                var users = _UserManager.Users.Select(e => new UserVM
                {
                    Id = e.Id,
                    FName = e.FName,
                    LName = e.LName,
                    Email = e.Email,
                    Roles = _UserManager.GetRolesAsync(e).Result
                });
                return View(users);
            }
            var user = await _UserManager.FindByEmailAsync(Email.Trim());
            if (user == null)
            {
                return View(Enumerable.Empty<UserVM>());
            }
            var mappedUser = new UserVM
            {
                Id = user.Id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles = await _UserManager.GetRolesAsync(user)
            };
            return View(new List<UserVM> { mappedUser });
        }
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            var user = await _UserManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var mappedUser = _mapper.Map<UserVM>(user);
            mappedUser.Roles = await _UserManager.GetRolesAsync(user);
            return View(ViewName, mappedUser);
        }
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, nameof(Update));
        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, nameof(Delete));
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UserVM userVM)
        {
            if (id != userVM.Id)
                return BadRequest();
            if (!ModelState.IsValid) return View(userVM);

            try
            {
                var user = await _UserManager.FindByIdAsync(id);
                if (user == null) return NotFound();
                user.FName = userVM.FName;
                user.LName = userVM.LName;
                user.Email = userVM.Email;
                await _UserManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, UserVM userVM)
        {
            if (id != userVM.Id)
                return BadRequest();
            if (!ModelState.IsValid) return View(userVM);

            try
            {
                var user = await _UserManager.FindByIdAsync(id);
                if (user == null) return NotFound();
                
                await _UserManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(userVM);
        }
    }

}