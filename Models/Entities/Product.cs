namespace GraphQlAspNetCoreDemo.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool Discontinued { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}