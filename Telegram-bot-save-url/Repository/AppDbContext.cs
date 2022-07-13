using System;
using Microsoft.EntityFrameworkCore;
using App.Repository.Entities;

namespace App.Repository
{
	public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UsersSessions { get; set; }
    }
}