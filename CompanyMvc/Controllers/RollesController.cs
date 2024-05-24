using AutoMapper;
using Company.DAL.Entities;
using CompanyMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PL.ViewModels;

namespace PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RollesController : Controller
    {
        private readonly UserManager<AppUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly IMapper _Mapper;

        public RollesController(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> RoleManager)
        {
            _UserManager = userManager;
            _Mapper = mapper;
            _RoleManager = RoleManager;
        }

        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var roles = _RoleManager.Roles.Select(e => new RoleViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                });
                return View(roles);
            }
            var Role = await _RoleManager.Roles.Where(e => e.Name.ToLower().Contains(name.ToLower())).Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name,
            }).ToListAsync();

            if (Role == null)
            {
                return View(Enumerable.Empty<RoleViewModel>());
            }

            return View(Role);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedRole = _Mapper.Map<IdentityRole>(model);
                var result = await _RoleManager.CreateAsync(mappedRole);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Details(string id, string ViewName)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();
            var Role = await _RoleManager.FindByIdAsync(id);
            if (Role == null) return NotFound();

            var mappedRole = _Mapper.Map<RoleViewModel>(Role);
            return View(ViewName, mappedRole);
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
        public async Task<IActionResult> Update([FromRoute] string id, RoleViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            try
            {
                var Role = await _RoleManager.FindByIdAsync(model.Id);
                if (Role == null) return NotFound();
                Role.Name = model.Name;
                await _RoleManager.UpdateAsync(Role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);

        }

        [HttpPost]

        public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel model)
        {
            if (id != model.Id) return BadRequest();
            try
            {
                var Role = await _RoleManager.FindByIdAsync(model.Id);
                if (Role == null) return NotFound();
                await _RoleManager.DeleteAsync(Role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string id)
        {
            ViewData["RoleId"] = id;
            var Users = await _UserManager.Users.ToListAsync();
            var Role = await _RoleManager.FindByIdAsync(id);
            if (Role is null) return NotFound();

            var UsersInRole = new List<UserInRoleVM>();

            foreach (var User in Users)
            {
                var MappedRoleUser = new UserInRoleVM
                {
                    UserId = User.Id,
                    UserName = User.UserName
                };

                if (await _UserManager.IsInRoleAsync(User, Role.Name))
                {
                    MappedRoleUser.IsSelected = true;
                }
                else
                {
                    MappedRoleUser.IsSelected = false;

                }
                UsersInRole.Add(MappedRoleUser);
            }

            return View(UsersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId, List<UserInRoleVM> users)
        {
            var Role = await _RoleManager.FindByIdAsync(RoleId);
            if (Role is null) return NotFound();


            if (!ModelState.IsValid) return View(users);
            else
            {
                foreach (var user in users)
                {
                    var User = await _UserManager.FindByIdAsync(user.UserId);
                    if (User is null) return NotFound();

                    if (user.IsSelected && !await _UserManager.IsInRoleAsync(User, Role.Name))
                    {
                        await _UserManager.AddToRoleAsync(User, Role.Name);
                    }
                    else if (!user.IsSelected && await _UserManager.IsInRoleAsync(User, Role.Name))
                    {
                        await _UserManager.RemoveFromRoleAsync(User, Role.Name);

                    }
                }
                return RedirectToAction("Update",new {id= RoleId});
            }
        }
    }
}
