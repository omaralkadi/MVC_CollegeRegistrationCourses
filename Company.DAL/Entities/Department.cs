using System.ComponentModel.DataAnnotations;

namespace Company.DAL.Entities
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is Required")]
        public int Code { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee> Employee { get; set; } = new List<Employee>();
    }
}
