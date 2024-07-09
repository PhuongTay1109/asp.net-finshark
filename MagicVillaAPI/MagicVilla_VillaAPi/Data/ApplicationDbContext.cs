using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Villas là tên table trong SQL server
        // query sẽ dùng LINQ và LINQ sẽ được tự động chuyển thành SQL queries bởi entity framework
        public DbSet<Villa> Villas { get; set; }
    }
}
