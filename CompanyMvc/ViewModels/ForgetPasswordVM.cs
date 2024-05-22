using System.ComponentModel.DataAnnotations;

namespace CompanyMvc.ViewModels
{
    public class ForgetPasswordVM
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
