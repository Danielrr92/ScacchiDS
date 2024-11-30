using Microsoft.EntityFrameworkCore;
using ScacchiDS.Server.Models;

namespace ScacchiDS.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
