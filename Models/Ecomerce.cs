using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Project.Models
{
    public class Ecomerce:IdentityDbContext<ApplicationUser>
    {
        public Ecomerce():base()
        {

        }
        public Ecomerce(DbContextOptions options): base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Ecomerce;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            modelBuilder.Entity<OrderDetails>().HasKey(o => new { o.OrderID, o.ProductID });
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

    }
}
