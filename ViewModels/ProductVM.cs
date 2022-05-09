using Project.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class ProductVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string Img { get; set; }
        public int Rate { get; set; }

        [Display(Name="Category")]
        public int CategoryID { get; set; }

        public List<Category> Category { get; set; }
    }
}
