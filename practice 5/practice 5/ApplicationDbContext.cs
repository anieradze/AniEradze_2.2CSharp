using Microsoft.EntityFrameworkCore;
using ExcelImportExport.Models;

namespace ExcelImportExport.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Personali> Personali { get; set; }
    }
}
