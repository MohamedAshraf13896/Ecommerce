using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }

        //Navigation Properties
        public List<Order> orders { get; set; }
    }
}
