using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using userIdentity.Models;

namespace userIdentity.Data
{
    public class CoursesContext :IdentityDbContext
    {
        public CoursesContext(DbContextOptions<CoursesContext> options) : base(options)
        {

        }
        public DbSet<userAuth> Users { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItems> cartItems { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseSqlServer("Server=DESKTOP-JD76U9C;Database=courses2;Trusted_Connection=True");
        }
    }
}
