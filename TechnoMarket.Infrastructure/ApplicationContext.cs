using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }


        private readonly bool isTestingEnviroment;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, bool isTestingEnviroment = false) : base(options)
        {
            this.isTestingEnviroment = isTestingEnviroment;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la entidad Product
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id); // Define la clave primaria para Product

            /*modelBuilder.Entity<Store>()
                .HasMany(s => s.Inventory) //una tienda tiene muchos productos
                .WithOne(p => p.Store) //un producto viene de una tieda
                .HasForeignKey(p => p.StoreId);*/

            modelBuilder
                .Entity<User>()
                .Property(u => u.Role)
                .HasConversion(new EnumToStringConverter<UserRole>());

            modelBuilder.Entity<User>()
            .HasOne(u => u.Store)
            .WithOne(s => s.Owner)
            .HasForeignKey<Store>(s => s.idOwner);
        }
    }
}
