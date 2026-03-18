using Consultorio.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; // Adicionado para garantir o uso de IEnumerable

namespace Consultorio.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consultorioo> Consultorioos { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

