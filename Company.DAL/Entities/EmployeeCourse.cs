using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Entities
{
    public class EmployeeCourse
    {
        public string CourseId { get; set; }
        public Course? Course { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
