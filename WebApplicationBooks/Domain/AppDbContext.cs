using Microsoft.EntityFrameworkCore;
using WebApplicationBooks.Domain.Repository.Entity;

namespace WebApplicationBooks.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
