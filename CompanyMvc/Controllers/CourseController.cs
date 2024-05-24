using AutoMapper;
using Company.BLL.Interface;
using Company.DAL.Entities;
using CompanyMvc.Utilities;
using CompanyMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMvc.Controllers
{
    public class CourseController : Controller
    {
        private readonly IMapper _mapper;
        public IUnitOfWork _unitOfWork { get; }

        public CourseController(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string? SeachValue, int pageNumber = 1, int pageSize = 5)
        {
            IEnumerable<Course> courses;

            if (string.IsNullOrWhiteSpace(SeachValue))
            {
                courses = await _unitOfWork.CourseRepo.GetAllAsync();
            }
            else
            {
                courses = _unitOfWork.CourseRepo.GetAllByName(e => e.Name.ToLower().Contains(SeachValue.ToLower()));

            }
            // Set pagination properties
            var totalRecords = courses.Count();

            var pagedEmployees = courses
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                 .ToList();


            var viewModel = new PaginationVM<Course>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                // This part is assuming you can assign your paginated list to a collection property in the view model
                Entity = pagedEmployees
            };

            return View(viewModel);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.CourseRepo.AddAsync(course);
                await _unitOfWork.Complete();
                TempData["Message"] = "Course Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        [HttpGet]
        public Task<IActionResult> Details(string? id) => ReturnViewWithEmployee(id, nameof(Details));


        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            return await ReturnViewWithEmployee(id, nameof(Update));
        }


        [HttpPost]
        public async Task<IActionResult> Update(Course course, [FromRoute] string? id)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                
                _unitOfWork.CourseRepo.Update(course);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }

            return View(course);
        }

        [HttpGet]
        public Task<IActionResult> Delete(string? id) => ReturnViewWithEmployee(id, nameof(Delete));

        [HttpPost]
        public async Task<IActionResult> Delete(Course course, string? id)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {

                _unitOfWork.CourseRepo.Delete(course);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        private async Task<IActionResult> ReturnViewWithEmployee(string? id, string ViewName)
        {
            if (id is null)
                return BadRequest();

            var course = await _unitOfWork.CourseRepo.GetByIdStringAsync(id);

            if (course is null)
                return NotFound();
            return View(ViewName, course);

        }
    }
}
