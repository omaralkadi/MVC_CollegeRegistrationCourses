using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Entities
{
    public class AppUserCourse
    {
        public string? CourseId { get; set; }
        public Course Course { get; set; }

        public string? UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
