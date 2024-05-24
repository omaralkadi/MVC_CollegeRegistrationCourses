namespace Company.BLL.Interface
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IEmployeeRepo EmployeeRepo { get; set; }
        public IDepartmentRepo DepartmentRepo { get; set; }
        public ICourseRepo CourseRepo { get; set; }
        Task<int> Complete();
    }
}
