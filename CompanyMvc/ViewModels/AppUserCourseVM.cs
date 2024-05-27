using Company.DAL.Entities;

namespace CompanyMvc.ViewModels
{
    public class AppUserCourseVM
    {
        public string CourseId { get; set; }
        public string UserId { get; set; }
        public bool Check { get; set; }
        public string EmpName { get; set; }
    }
}
