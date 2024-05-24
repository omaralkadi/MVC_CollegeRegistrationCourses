using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Entities
{
    public class Course
    {
        public string CourseId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,12)]
        public int Duration { get; set; }
        public ICollection<AppUserCourse>? AppUserCourse { get; set; }


    }
}
