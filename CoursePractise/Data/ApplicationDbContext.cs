using CoursePractise.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursePractise.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }

        public DbSet<CustomPage> CustomPage { get; set; }

        public DbSet<Product> Product { get; set; }
        
    }
}
