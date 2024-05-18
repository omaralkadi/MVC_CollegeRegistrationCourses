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
                return RedirectToAction(nameof(Index));

            }
            return View(department);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await _departmentRepo.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await _departmentRepo.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }

        [HttpPost]
        public IActionResult Update(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepo.Update(department);
                return RedirectToAction(nameof(Index));

            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await _departmentRepo.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(department);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepo.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

    }
}
