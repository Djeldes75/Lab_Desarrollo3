using DenunciasService.Models;
using Microsoft.EntityFrameworkCore;

namespace DenunciasService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}
