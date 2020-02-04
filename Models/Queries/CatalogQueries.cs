using System.Linq;
using System.Threading.Tasks;
using GraphQlAspNetCoreDemo.Models.Entities;
using GraphQlAspNetCoreDemo.Models.Services;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;

namespace GraphQlAspNetCoreDemo.Models.Queries
{
    public class CatalogQueries
    {

        [UsePaging, UseFiltering, UseSorting]
        public IQueryable<Product> Products([Service] CatalogDbContext dbContext) {
            return dbContext.Products.Include(p => p.Category);
        }

        [UsePaging, UseFiltering, UseSorting]
        public IQueryable<Category> Categories([Service] CatalogDbContext dbContext) {
            return dbContext.Categories;
        }

        public async Task<Product> Product(int id, [Service] CatalogDbContext dbContext)
        {
            return await dbContext.Products.FindAsync(id);
        }

        public async Task<Category> Category(int id, [Service] CatalogDbContext dbContext)
        {
            return await dbContext.Categories.FindAsync(id);
        }
    }
}