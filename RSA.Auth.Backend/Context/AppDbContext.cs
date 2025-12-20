using Microsoft.EntityFrameworkCore;
using RsaAuth.Backend.Models;

namespace RsaAuth.Backend.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().ToTable("users"); //explicit mapping
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        }
    }
}
