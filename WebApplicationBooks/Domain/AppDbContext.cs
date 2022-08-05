using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationBooks.Domain.Repository.Entity;
using WebApplicationBooks.Models;

namespace WebApplicationBooks.Domain
{
    public class AppDbContext : IdentityDbContext<AccountUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}