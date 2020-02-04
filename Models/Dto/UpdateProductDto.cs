namespace GraphQlAspNetCoreDemo.Models.Dto
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public bool Discontinued { get; set; }
    }
}