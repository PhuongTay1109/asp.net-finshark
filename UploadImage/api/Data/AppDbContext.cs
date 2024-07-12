using Microsoft.EntityFrameworkCore;
using MinimalImageUploadAPI.Models;

namespace MinimalImageUploadAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ImageModel> Images { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageModel>()
                .Property(i => i.Id)
                .HasDefaultValueSql("NEWID()");
        }
    }
}
