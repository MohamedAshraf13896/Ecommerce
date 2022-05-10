using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string Img { get; set; }
        public int Rate { get; set; }
        //Foreign Keys
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        //Navigation Properties
        public Category Category { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }

        [NotMapped]
        public IFormFile imgFile { get; set; }
    }
}
