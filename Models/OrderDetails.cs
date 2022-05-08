using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class OrderDetails
    {
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
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
