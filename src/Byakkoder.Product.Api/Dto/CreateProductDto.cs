namespace Byakkoder.Product.Api.Dto
{
    public class CreateProductDto
    {
        public string ProductId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string StatusName { get; set; } = null!;

        public long Stock { get; set; }

        public double Price { get; set; }
    }
}
