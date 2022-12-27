using CoursePractise.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoursePractise.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }

        public DbSet<CustomPage> CustomPage { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ApplicationUser> User { get; set; }

    }
}
