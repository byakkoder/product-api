using System.ComponentModel.DataAnnotations;

namespace Byakkoder.Product.Api.Dto
{
    public class UpdateProductDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductId { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string StatusName { get; set; } = null!;

        [Required]
        public long Stock { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
}
