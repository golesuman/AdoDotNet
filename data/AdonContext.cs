
using adonet.Models;
using Microsoft.EntityFrameworkCore;

namespace adonet.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        // Add your DbSet properties here
    }
}