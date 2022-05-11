using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        [Display(Name = "Category Deascription")]
        public string Description { get; set; }
        [Display(Name = "Catrgory Image")]
        public string Img { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        //Foreign Keys
        //Navigation Properties
        public virtual List<Product> Products { get; set; }

        //unmapped
        [NotMapped]
        [Display(Name = "Category Image")]
        [Required(ErrorMessage = "Image is required")]
        public IFormFile imgFile { get; set; }
    }
}
