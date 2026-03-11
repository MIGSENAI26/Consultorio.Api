using Consultorio.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define o CPF como índice único
            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.Cpf)
                .IsUnique();

            // Define o E-mail como índice único
            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.Email)
                .IsUnique();
        }
    }
}
