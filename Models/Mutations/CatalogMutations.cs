using System.Linq;
using System.Threading.Tasks;
using GraphQlAspNetCoreDemo.Models.Dto;
using GraphQlAspNetCoreDemo.Models.Entities;
using GraphQlAspNetCoreDemo.Models.Services;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace GraphQlAspNetCoreDemo.Models.Mutations
{
    public class CatalogMutations
    {
        public async Task<Product> CreateProduct(CreateProductDto input, [Service]CatalogDbContext dbContext)
        {
            var product = new Product
            {
                Name = input.Name,
                Price = input.Price,
                CategoryId = input.CategoryId
            };
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return product;
        }
        public async Task<Product> UpdateProduct(int id, UpdateProductDto input, [Service]CatalogDbContext dbContext)
        {
            var product = await dbContext.Products.FindAsync(id);
            product.Name = input.Name;
            product.Price = input.Price;
            product.Quantity = input.Quantity;
            product.CategoryId = input.CategoryId;
            product.Discontinued = input.Discontinued;
            await dbContext.SaveChangesAsync();
            return product;
        }
        public async Task<bool> DeleteProduct(int id, [Service] CatalogDbContext dbContext)
        {
            var product = await dbContext.Products.FindAsync(id);
            dbContext.Remove(product);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}