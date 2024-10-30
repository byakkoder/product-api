using System.ComponentModel.DataAnnotations;

namespace Byakkoder.Product.Api.Dto
{
    public class UpdateProductDto
    {
        /// <summary>
        /// Internal record id.
        /// </summary>
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// Business product code to identify a product and to get its discount from an external API (an integer value is recommended in order to get the mocked values). This value decouples the responsability of the internal record (id field).
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ProductId { get; set; } = null!;

        /// <summary>
        /// Product name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Product detailed description.
        /// </summary>
        [Required]
        [MaxLength(250)]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Product status. Only Active or Inactive values are admitted.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string StatusName { get; set; } = null!;

        /// <summary>
        /// Product stock.
        /// </summary>
        [Required]
        public long Stock { get; set; }

        /// <summary>
        /// Product price without any discount.
        /// </summary>
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
}
