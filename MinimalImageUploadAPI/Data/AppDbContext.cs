using Microsoft.EntityFrameworkCore;
using MinimalImageUploadAPI.Models;

namespace MinimalImageUploadAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ImageModel> Images { get; set; }
    }
}
