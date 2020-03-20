using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompositeNullableForeignKeySample.Dtos;
using CompositeNullableForeignKeySample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CompositeNullableForeignKeySample
{
    internal class Program
    {
        private static readonly Guid _tenant1Id = Guid.Parse("cb7bc554-946e-49a6-9e0e-f37dce2b4e87");
        private static readonly Guid _tenant2Id = Guid.Parse("6a4aa310-673e-4058-b576-aed438dede3b");
        private static readonly Guid _tenant3Id = Guid.Parse("7d70c3c3-3a1c-4263-b074-631169e93d6e");

        private static readonly MapperConfiguration _mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductDto>();
            cfg.CreateMap<ProductCategory, ProductCategoryDto>();
        });

        private static async Task Main(string[] args)
        {
            #region Init Database

            using (var context = new SampleContext())
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                await context.Database.MigrateAsync();
            }

            #endregion Init Database

            #region Add some data

            using (var context = new SampleContext())
            {
                var tenant1Category = new ProductCategory { TenantId = _tenant1Id, ProductCategoryName = "Tenant 1 - Category ABC" };
                var tenant2Category = new ProductCategory { TenantId = _tenant2Id, ProductCategoryName = "Tenant 2 - Category XYZ" };

                await context.ProductCategories.AddRangeAsync(tenant1Category, tenant2Category);

                var tenant1Product = new Product
                {
                    TenantId = _tenant1Id,
                    ProductCategoryId = tenant1Category.ProductCategoryId,
                    ProductName = "Tenant 1 - ABC - Product 11"
                };

                var tenant2Product = new Product
                {
                    TenantId = _tenant2Id,
                    ProductCategoryId = tenant2Category.ProductCategoryId,
                    ProductName = "Tenant 2 - XYZ - Product 22"
                };

                await context.Products.AddRangeAsync(tenant1Product, tenant2Product);

                await context.SaveChangesAsync();
            }

            #endregion Add some data

            #region Add more data

            using (var context = new SampleContext())
            {
                var tenant3Product = new Product

                {
                    TenantId = _tenant3Id,
                    ProductCategoryId = null, // It's OK
                    ProductName = "This product also ablo to save"
                };

                await context.Products.AddAsync(tenant3Product);

                await context.SaveChangesAsync();
            }

            #endregion Add more data

            #region Read data without mapping

            using (var context = new SampleContext())
            {
                Console.WriteLine();
                Console.WriteLine("Data reading without AutoMapper");
                Console.WriteLine("-------------------------------");

                var query = context.Products;
                var products = await query.ToListAsync();

                foreach (var product in products)
                {
                    Console.WriteLine(product.ToString());
                }
            }

            #endregion Read data without mapping

            #region Read data with automapper (the problem is here)

            using (var context = new SampleContext())
            {
                Console.WriteLine();
                Console.WriteLine("Data reading with AutoMapper");
                Console.WriteLine("----------------------------");

                var query = context.Products;
                var products = await query.ProjectTo<ProductDto>(_mapperConfiguration).ToListAsync();

                foreach (var product in products)
                {
                    Console.WriteLine(product.ToString());
                }
            }

            #endregion Read data with automapper (the problem is here)
        }
    }
}