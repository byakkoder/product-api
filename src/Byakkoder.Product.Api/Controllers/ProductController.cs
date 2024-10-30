using Byakkoder.Product.Api.Dto;
using Byakkoder.Product.Api.Interfaces;
using Byakkoder.Product.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Byakkoder.Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Fields
        
        private readonly IProductHandler _productHandler;
        private readonly ILogger<ProductController> _logger;

        #endregion

        #region Constants

        private const string InternalErrorMessage = "An internal error has ocurred. Contact Administrator for details.";
        private const string DetailsErrorMessage = "An internal error has ocurred: {0}";

        #endregion

        #region Constructor

        public ProductController(
            IProductHandler productHandler,
            ILogger<ProductController> logger)
        {
            _productHandler = productHandler;
            _logger = logger;
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Get a product by its internal id.
        /// </summary>
        /// <param name="id">Internal identifier of the product (record id).</param>
        /// <returns>Product wit detailed information like discount and final price.</returns>
        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                ProductDto productDto = await _productHandler.GetById(id);

                return Ok(productDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(DetailsErrorMessage, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, InternalErrorMessage);
            }
        }

        /// <summary>
        /// Create a product with its mandatory fields.
        /// </summary>
        /// <param name="createProductDto">Product to create.</param>
        /// <returns>Created product with its internal id.</returns>
        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductDto createProductDto)
        {
            try
            {
                BasicProductDto createdProductDto = await _productHandler.Insert(createProductDto);

                return CreatedAtAction(nameof(Get), new { id = createdProductDto.Id }, createdProductDto);
            }
            catch (ItemExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(DetailsErrorMessage, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, InternalErrorMessage);
            }
        }

        /// <summary>
        /// Updates the information for a product.
        /// </summary>
        /// <param name="id">Product internal id.</param>
        /// <param name="updateProductDto">Product to update.</param>
        /// <returns>Successfull operation.</returns>
        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductDto updateProductDto)
        {
            if (id != updateProductDto.Id)
            {
                return BadRequest("Product id and url id doesn't match.");
            }

            try
            {
                await _productHandler.Update(updateProductDto);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ItemExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(DetailsErrorMessage, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, InternalErrorMessage);
            }
        } 

        #endregion
    }
}
