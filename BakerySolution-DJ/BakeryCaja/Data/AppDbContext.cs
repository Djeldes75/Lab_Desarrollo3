using Microsoft.EntityFrameworkCore;
using BakeryCaja.Models;

namespace BakeryCaja.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CashTransaction> CashTransactions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
