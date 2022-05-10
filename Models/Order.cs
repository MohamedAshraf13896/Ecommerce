using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Index(nameof(Order.OrderNumber), IsUnique = true)]
    public class Order
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Order Number is required")]
        [Display(Name = "Order Number")]
        public int OrderNumber { get; set; }


        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Order Date is required")]
        [Column(TypeName = "date")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }


        [Required(ErrorMessage = "Ship Date is required")]
        [Column(TypeName = "date")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Ship Date")]
        public DateTime ShipDate { get; set; }


        [Display(Name = "Sub Total")]
        public decimal SubTotal { get; set; }


        [Display(Name = "Discount")]
        public decimal Discount { get; set; } = 0;


        [Required(ErrorMessage = "Order Tax is required")]
        [Display(Name = "Order Tax")]
        public decimal OrderTax { get; set; }


        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }


        [Display(Name = "Is Paid")]
        public bool IsPaid { get; set; }


        //Foreign Keys
        [ForeignKey("Shipper")]
        public int ShipperID { get; set; }

        [ForeignKey("Payment")]
        public int PaymentID { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

        //Navigation Properties
        public Shipper Shipper { get; set; }
        public Payment Payment { get; set; }
        public ApplicationUser Customer { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }

    }
}
