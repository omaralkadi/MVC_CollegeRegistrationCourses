using Company.BLL.Interface;
using Company.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMvc.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepo.GetAllAsync();
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
                await _unitOfWork.DepartmentRepo.AddAsync(department);
                await _unitOfWork.Complete();
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
        public async Task<IActionResult> Update(Department department, [FromRoute] int? id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepo.Update(department);
                await _unitOfWork.Complete();
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
                try
                {
                    _unitOfWork.DepartmentRepo.Delete(department);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "This Department Has Enrolled Employees");
                }

            }
            return View(department);
        }

        private async Task<IActionResult> ReturnViewWithDepartment(int? id, string ViewName)
        {
            if (id is null)
                return BadRequest();
            var department = await _unitOfWork.DepartmentRepo.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(ViewName, department);

        }

    }
}
