using System.ComponentModel.DataAnnotations;

namespace JobFinder.ViewModels
{
	public class LoginViewModel
    {
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Електрона адреса є обов'язковим полем")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
