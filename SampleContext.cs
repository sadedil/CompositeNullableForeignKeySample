using CompositeNullableForeignKeySample.Models;
using Microsoft.EntityFrameworkCore;

namespace CompositeNullableForeignKeySample
{
    public class SampleContext : DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);

            // ##########################################################
            // If I commented out HasForeignKey and HasPrincipalKey lines
            // ProjectTo works without any problem
            // ##########################################################
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => new { p.TenantId, p.ProductCategoryId }) // Comment me out to work ProjectTo
                .HasPrincipalKey(pc => new { pc.TenantId, pc.ProductCategoryId }); // Comment me out to work ProjectTo

            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => pc.ProductCategoryId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // If you want to try with PostgreSQL, you can toggle commented lines below

            //optionsBuilder.UseNpgsql("UserID=postgres;Password=123456;Host=localhost;Port=5432;Database=SampleDb;Pooling=true;");
            optionsBuilder.UseSqlServer("Server=(localdb)\\db1;Database=SampleDb;Integrated Security=true;");
        }
    }
}