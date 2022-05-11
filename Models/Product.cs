using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Color")]
        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "Unit Price is required")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Discount")]
        [Required(ErrorMessage = "Discount is required")]
        public decimal Discount { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Image is required")]
        public string Img { get; set; }

        public int Rate { get; set; } //why should i enter rate
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
