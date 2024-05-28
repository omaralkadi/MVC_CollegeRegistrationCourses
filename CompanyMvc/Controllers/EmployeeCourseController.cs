using Company.BLL.Interface;
using Company.BLL.Repository;
using Company.DAL.Context;
using Company.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyMvc.Controllers
{
    [Authorize(Roles ="Admin")]
    public class EmployeeCourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DataContext _dataContext { get; }

        public EmployeeCourseController(IUnitOfWork unitOfWork, DataContext dataContext)
        {
            _unitOfWork = unitOfWork;
            _dataContext = dataContext;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Employees = await _unitOfWork.EmployeeRepo.GetAllAsync();
            ViewBag.Courses = await _unitOfWork.CourseRepo.GetAllAsync();
            var EmployeeCourses = await _unitOfWork.EmployeeCourse.GetAllAsync();
            return View(EmployeeCourses);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Employees = await _unitOfWork.EmployeeRepo.GetAllAsync();
            ViewBag.Courses = await _unitOfWork.CourseRepo.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCourse model)
        {
            if (ModelState.IsValid)
            {

                await _unitOfWork.EmployeeCourse.AddAsync(model);

                try
                {
                    await _unitOfWork.Complete();
                    TempData["Message"] = "Course Added Successfully";

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "This Instructor Already InRolled In This Course");
                    ViewBag.Employees = await _unitOfWork.EmployeeRepo.GetAllAsync();
                    ViewBag.Courses = await _unitOfWork.CourseRepo.GetAllAsync();
                    return View(model);
                }

                return RedirectToAction(nameof(Index));

            }
            ViewBag.Employees = await _unitOfWork.EmployeeRepo.GetAllAsync();
            ViewBag.Courses = await _unitOfWork.CourseRepo.GetAllAsync();

            return View(model);

        }

        [HttpGet]
        public Task<IActionResult> Details(int? id, string? CourseId) => ReturnViewWithDepartment(id, CourseId, nameof(Details));


        [HttpGet]
        public Task<IActionResult> Update(int? id, string? CourseId) => ReturnViewWithDepartment(id, CourseId, nameof(Update));

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeCourse employeeCourse, string OldCourId, int OldEmpId)
        {
            var OldEmpCourse = await _unitOfWork.EmployeeCourse.GetByCompositeKeyAsync(OldCourId, OldEmpId);
            if (OldEmpCourse is not null)
            {
                _unitOfWork.EmployeeCourse.Delete(OldEmpCourse);
            }

            if (ModelState.IsValid)
            {

                try
                {
                    await _unitOfWork.EmployeeCourse.AddAsync(employeeCourse);
                    await _unitOfWork.Complete();
                    TempData["Message"] = "Course Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Concurrency error occurred. Try again.");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "This Instructor is already enrolled in this course.");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "An error occurred while updating the course. Please try again.");
                }
            }

            ViewBag.Employees = await _unitOfWork.EmployeeRepo.GetAllAsync();
            ViewBag.Courses = await _unitOfWork.CourseRepo.GetAllAsync();
            return View(employeeCourse);
        }


        [HttpGet]
        public Task<IActionResult> Delete(int? id, string? CourseId) => ReturnViewWithDepartment(id, CourseId, nameof(Delete));

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeCourse employeeCourse, int? id)
        {
            if (id != employeeCourse.EmployeeId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.EmployeeCourse.Delete(employeeCourse);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return View(employeeCourse);
        }

        private async Task<IActionResult> ReturnViewWithDepartment(int? id, string? CourseId, string ViewName)
        {
            if (id is null)
                return BadRequest();
            var employeeCourse = await _unitOfWork.EmployeeCourse.GetByCompositeKeyAsync(CourseId, id.Value);
            if (employeeCourse is null)
                return NotFound();
            ViewBag.Employees = await _unitOfWork.EmployeeRepo.GetAllAsync();
            ViewBag.Courses = await _unitOfWork.CourseRepo.GetAllAsync();
            return View(ViewName, employeeCourse);

        }
    }
}
