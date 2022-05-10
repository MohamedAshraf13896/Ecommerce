using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Payment
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Type is required")]
        [Display(Name = "Payment Type")]
        public string Type { get; set; }

        //Foreign Keys

        //Navigation Properties
        public virtual List<Order>Orders { get; set; }

    }
}
