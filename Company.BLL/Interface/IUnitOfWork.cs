namespace Company.BLL.Interface
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IEmployeeRepo EmployeeRepo { get; set; }
        public IDepartmentRepo DepartmentRepo { get; set; }
        public ICourseRepo CourseRepo { get; set; }
        public IEmployeeCourse EmployeeCourse { get; set; }
        public IAppUserCourse AppUserCourse { get; set; }


        Task<int> Complete();
    }
}
