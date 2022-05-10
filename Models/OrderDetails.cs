using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class OrderDetails
    {
        [Display(Name = "Order Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Display(Name = "Order Discount")]
        [Required(ErrorMessage = "Discount is required")]
        public decimal Discount { get; set; }

        [Display(Name = "LinePrice")]
        public decimal LinePrice { get; set; }

        //Foreign Keys
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }

        //Navigation Properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
