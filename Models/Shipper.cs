using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Shipper
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Company Name is required")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^ 01[0 - 2, 5]\d{11}$",
         ErrorMessage = "Characters are not allowed.")]
        public string PhoneNumber { get; set; }

        //Foreign Keys

        //Navigation Properties
        public virtual List<Order>Orders { get; set; }
    }
}
