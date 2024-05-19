using Company.BLL.Interface;
using Company.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CompanyMvc.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepo _departmentRepo;

        public DepartmentController(IDepartmentRepo repository)
        {
            _departmentRepo = repository;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentRepo.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepo.AddAsync(department);
                TempData["Message"] = "Department Created Successfully";
                return RedirectToAction(nameof(Index));

            }
            return View(department);
        }
        [HttpGet]
        public Task<IActionResult> Details(int? id) => ReturnViewWithDepartment(id, nameof(Details));


        [HttpGet]
        public Task<IActionResult> Update(int? id) => ReturnViewWithDepartment(id, nameof(Update));


        [HttpPost]
        public IActionResult Update(Department department, [FromRoute] int? id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _departmentRepo.Update(department);
                return RedirectToAction(nameof(Index));

            }
            return View(department);
        }

        [HttpGet]
        public Task<IActionResult> Delete(int? id) => ReturnViewWithDepartment(id, nameof(Delete));

        [HttpPost]
        public async Task<IActionResult> Delete(Department department, int? id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _departmentRepo.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        private async Task<IActionResult> ReturnViewWithDepartment(int? id, string ViewName)
        {
            if (id is null)
                return BadRequest();
            var department = await _departmentRepo.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(ViewName, department);

        }

    }
}
