using Company.BLL.Interface;
using Company.DAL.Context;

namespace Company.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IEmployeeRepo EmployeeRepo { get; set; }
        public IDepartmentRepo DepartmentRepo { get; set; }
        public ICourseRepo CourseRepo { get; set; }
        public IEmployeeCourse EmployeeCourse { get; set; }
        public IAppUserCourse AppUserCourse { get; set; }

        public UnitOfWork(IEmployeeRepo employeeRepo, IDepartmentRepo departmentRepo, ICourseRepo courseRepo,
            IEmployeeCourse employeeCourse,IAppUserCourse appUserCourse , DataContext context)
        {
            EmployeeRepo = employeeRepo;
            DepartmentRepo = departmentRepo;
            CourseRepo = courseRepo;
            EmployeeCourse = employeeCourse;
            AppUserCourse = appUserCourse;
            _context = context;
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}
