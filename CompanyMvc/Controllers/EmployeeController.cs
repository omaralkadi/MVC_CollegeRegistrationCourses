using AutoMapper;
using Company.BLL.Interface;
using Company.DAL.Entities;
using CompanyMvc.Utilities;
using CompanyMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        public IUnitOfWork _unitOfWork { get; }

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string? SeachValue)
        {
            if (string.IsNullOrWhiteSpace(SeachValue))
            {
                var employees = await _unitOfWork.EmployeeRepo.GetAllAsync();
                var MappedEmployees = _mapper.Map<IEnumerable<EmployeeVM>>(employees);
                return View(MappedEmployees);
            }
            var returnedEmps = _unitOfWork.EmployeeRepo.GetAllByName(e => e.Name.ToLower().Contains(SeachValue.ToLower()));
            var ReturnedMapped = _mapper.Map<IEnumerable<EmployeeVM>>(returnedEmps);
            return View(ReturnedMapped);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.departments = await _unitOfWork.DepartmentRepo.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");
                var MappedEmployees = _mapper.Map<Employee>(employeeVM);
                await _unitOfWork.EmployeeRepo.AddAsync(MappedEmployees);
                if(await _unitOfWork.Complete()<=0)
                {
                    DocumentSetting.DeleteFile(employeeVM.ImageName,"Images");
                }
                TempData["Message"] = "Employee Created Successfully";
                return RedirectToAction(nameof(Index));

            }
            ViewBag.departments = await _unitOfWork.DepartmentRepo.GetAllAsync();
            return View(employeeVM);
        }
        [HttpGet]
        public Task<IActionResult> Details(int? id) => ReturnViewWithEmployee(id, nameof(Details));


        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.departments = await _unitOfWork.DepartmentRepo.GetAllAsync();

            return await ReturnViewWithEmployee(id, nameof(Update));
        }


        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM employeeVM, [FromRoute] int? id)
        {
            if (id != employeeVM.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                if (employeeVM.Image is not null)
                {
                    DocumentSetting.DeleteFile(employeeVM.ImageName,"Images");
                    employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");

                }
                var MappedEmployees = _mapper.Map<Employee>(employeeVM);

                _unitOfWork.EmployeeRepo.Update(MappedEmployees);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            ViewBag.departments = await _unitOfWork.DepartmentRepo.GetAllAsync();

            return View(employeeVM);
        }

        [HttpGet]
        public Task<IActionResult> Delete(int? id) => ReturnViewWithEmployee(id, nameof(Delete));

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeVM employeeVM, int? id)
        {
            if (id != employeeVM.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var MappedEmployees = _mapper.Map<Employee>(employeeVM);

                _unitOfWork.EmployeeRepo.Delete(MappedEmployees);
                if (await _unitOfWork.Complete() > 0)
                {
                    DocumentSetting.DeleteFile(employeeVM.ImageName, "Images");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        private async Task<IActionResult> ReturnViewWithEmployee(int? id, string ViewName)
        {
            if (id is null)
                return BadRequest();

            var employee = await _unitOfWork.EmployeeRepo.GetByIdAsync(id.Value);
            var MappedEmployees = _mapper.Map<EmployeeVM>(employee);

            if (employee is null)
                return NotFound();
            return View(ViewName, MappedEmployees);

        }

    }
}
