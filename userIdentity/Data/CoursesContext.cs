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
        public DbSet<CartItems> cartitem { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseSqlServer("Server=LAPTOP-BFFJ9SQ9;Database=courses2;Trusted_Connection=True");
        }
    }
}
