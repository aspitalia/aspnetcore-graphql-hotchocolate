using System.Diagnostics.CodeAnalysis;
using GraphQlAspNetCoreDemo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GraphQlAspNetCoreDemo.Models.Services
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(builder =>
            {
                builder.ToTable("Products");
                builder
                    .HasOne(product => product.Category)
                    .WithMany(category => category.Products)
                    .HasForeignKey(product => product.CategoryId);

                builder.Property(p => p.Price).HasConversion<float>();
            });

            modelBuilder.Entity<Category>(builder => 
            {
                builder.ToTable("Categories");
            });
        }
    }
}