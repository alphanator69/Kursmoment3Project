// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using Kursmoment3Project.Models;

namespace Kursmoment3Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
    }
}
