using JobFinder.Data;
using JobFinder.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Електрона адреса є обов'язковим полем")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Condirm password")]
        [Required(ErrorMessage = "Підтвердження пароля є обов'язковим полем")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Тип акаунта є обов'язковим полем")]
        public Roles Role{ get; set; }

        [Required(ErrorMessage = "Ім'я є обов'язковим полем")]
        [StringLength(20, ErrorMessage = "Максимальна довжина імені - 20 символів")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Прізвище є обов'язковим полем")]
        [StringLength(20, ErrorMessage = "Максимальна довжина прізвища - 20 символів")]
        public string Surname { get; set; }

    }
}
