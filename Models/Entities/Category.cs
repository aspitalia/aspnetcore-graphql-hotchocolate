using System.Collections.Generic;

namespace GraphQlAspNetCoreDemo.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}