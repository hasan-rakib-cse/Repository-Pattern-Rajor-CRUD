using Microsoft.EntityFrameworkCore;

namespace RepositoryPattern.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<RepositoryPattern.Models.Student> Student { get; set; } = default!;
    }
}
