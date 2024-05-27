using Company.BLL.Interface;
using Company.DAL.Context;
using Company.DAL.Entities;
using CompanyMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CompanyMvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DataContext _dataContext { get; }
        public UserManager<AppUser> _userManager { get; }

        public HomeController(ILogger<HomeController> logger,DataContext dataContext,IUnitOfWork unitOfWork , UserManager<AppUser> userManager)
        {
            _logger = logger;
            _dataContext = dataContext;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user=await _userManager.GetUserAsync(User);
            var UserId = user.Id;

            var employeeCourses=await _unitOfWork.AppUserCourse.GetAllAsync();
            var LoginUserCourses = employeeCourses.Where(e => e.UserId == UserId);
            return View(LoginUserCourses);
        }

        public async Task<IActionResult> InstructorCourses()
        {
            var employeeCourses = await _unitOfWork.EmployeeCourse.GetAllAsync();
            return View(employeeCourses);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
