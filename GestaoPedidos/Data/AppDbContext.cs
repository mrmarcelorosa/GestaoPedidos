using GestaoPedidos.DTO;
using GestaoPedidos.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestaoPedidos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração TPH
            modelBuilder.Entity<People>()
                .UseTphMappingStrategy()
                .HasDiscriminator<string>("person_type")
                .HasValue<Client>("Client")
                .HasValue<Employee>("Employee");

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Email)
                .IsUnique();

        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Verifique se a opção já foi configurada para evitar reconfiguração
            if (!optionsBuilder.IsConfigured)
            {
                // Defina a string de conexão diretamente aqui
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=GestaoPedidos;User Id=postgres;Password=postgres;");
            }
        }*/


        // public DbSet<People> People { get; set; }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
    }
}
