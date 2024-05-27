using Company.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace CompanyMvc.ViewModels
{
    public class EmployeeCourseVM
    {
        public string CourseId { get; set; }
        public bool Check { get; set; }
        public string CourseName { get; set; }
        public int Duration { get; set; }
        public string EmpName { get; set; }
        public string UserId { get; set; }

    }
}
