using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int OrderNumber { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal OrderTax { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        //Foreign Keys
        [ForeignKey("Shipper")]
        public int ShipperID { get; set; }

        [ForeignKey("Payment")]
        public int PaymentID { get; set; }
        //Navigation Properties
        public Shipper Shipper { get; set; }
        public Payment Payment { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }

    }
}
