using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class RegisterVM
    {
        [Required, MaxLength(256)]

        public string UserName { get; set; }

        [Required, MaxLength(256)]
        public string Address { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "User Role")]
        public string Role { get; set; }
    }
}
