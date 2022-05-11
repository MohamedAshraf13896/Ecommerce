using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class LoginVM
    {
        [Required,EmailAddress]
            public string Email { get; set; }

            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember Me")]
            public bool RememberMe { get; set; }
            public string ReturnUrl { get; set; }

    }
}
