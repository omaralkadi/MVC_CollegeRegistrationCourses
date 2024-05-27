using Company.BLL.Interface;
using Company.DAL.Entities;
using CompanyMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyMvc.Controllers
{
    [Authorize]
    public class AppUserCourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserManager<AppUser> _userManager { get; }

        public AppUserCourseController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> RegisterStudentInCourse()
        {
            var user = await _userManager.GetUserAsync(User);
            var EmpCourses = await _unitOfWork.EmployeeCourse.GetAllAsync();
            string currentUserId = user.Id;

            var ECourse = new List<EmployeeCourseVM>();

            foreach (var course in EmpCourses)
            {
                var MappedUserCourse = new EmployeeCourseVM()
                {
                    CourseName = course.Course.Name,
                    CourseId = course.CourseId,
                    Duration = course.Course.Duration,
                    EmpName = course.Employee.Name,
                    UserId = currentUserId
                };

                var UC = await _unitOfWork.AppUserCourse.GetByCompositeKeyAsync(course.CourseId, currentUserId, course.Employee.Name);
                if (UC is not null)
                {
                    MappedUserCourse.Check = true;
                }
                else
                {
                    MappedUserCourse.Check = false;
                }

                ECourse.Add(MappedUserCourse);
            }

           
            return View(ECourse);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudentInCourse(List<AppUserCourseVM> model)
        {
            if (ModelState.IsValid)
            {
                foreach (var UserCourse in model)
                {
                    var hasCourse = await _unitOfWork.AppUserCourse.GetByCompositeKeyAsync(UserCourse.CourseId,UserCourse.UserId,UserCourse.EmpName);

                    var MappedUserCourse = new AppUserCourse
                    {
                        CourseId=UserCourse.CourseId,
                        UserId=UserCourse.UserId,
                        InstructorName=UserCourse.EmpName
                    };

                    if(UserCourse.Check && hasCourse is null)
                    {
                        await _unitOfWork.AppUserCourse.AddAsync(MappedUserCourse);
                    }
                    else if(!UserCourse.Check && hasCourse is not null)
                    {
                        _unitOfWork.AppUserCourse.Delete(hasCourse);
                    }
                }
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(RegisterStudentInCourse));

            }
            return View(model);
        }
    }
}







