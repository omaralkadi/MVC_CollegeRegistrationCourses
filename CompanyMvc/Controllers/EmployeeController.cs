using Company.BLL.Interface;
using Company.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CompanyMvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeController(IEmployeeRepo repository)
        {
            _employeeRepo = repository;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepo.GetAllAsync();
            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepo.AddAsync(employee);
                TempData["Message"] = "Employee Created Successfully";
                return RedirectToAction(nameof(Index));

            }
            return View(employee);
        }
        [HttpGet]
        public Task<IActionResult> Details(int? id) => ReturnViewWithEmployee(id, nameof(Details));


        [HttpGet]
        public Task<IActionResult> Update(int? id) => ReturnViewWithEmployee(id, nameof(Update));


        [HttpPost]
        public IActionResult Update(Employee employee, [FromRoute] int? id)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _employeeRepo.Update(employee);
                return RedirectToAction(nameof(Index));

            }
            return View(employee);
        }

        [HttpGet]
        public Task<IActionResult> Delete(int? id) => ReturnViewWithEmployee(id, nameof(Delete));

        [HttpPost]
        public async Task<IActionResult> Delete(Employee employee, int? id)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _employeeRepo.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        private async Task<IActionResult> ReturnViewWithEmployee(int? id, string ViewName)
        {
            if (id is null)
                return BadRequest();
            var employee = await _employeeRepo.GetByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            return View(ViewName, employee);

        }

    }
}
