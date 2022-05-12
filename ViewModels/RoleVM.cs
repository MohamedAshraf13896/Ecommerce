using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class RoleVM
    {
        [Required]
        public string RoleName { get; set; }
    }
}
