using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public bool IsActive { get; set; }

        //Foreign Keys
        //Navigation Properties
        public virtual List<Product> Products { get; set; }

        //unmapped
        [NotMapped]
        public IFormFile imgFile { get; set; }
    }
}
