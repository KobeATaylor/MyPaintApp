using Microsoft.EntityFrameworkCore;
using PaintBackend.Models;
namespace PaintBackend.Data
{
    public class PaintDbContext : DbContext
    {
        public PaintDbContext(DbContextOptions<PaintDbContext> options)
            : base(options)
        {
        }
        public DbSet<Paint> Paintings { get; set; }
    }
}