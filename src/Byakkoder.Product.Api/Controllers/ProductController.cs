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
        private readonly IProductHandler _productHandler;

        public ProductController(IProductHandler productHandler)
        {
            _productHandler = productHandler;
        }

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
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post(ProductDto productDto)
        {
            try
            {
                ProductDto createdProductDto = await _productHandler.Insert(productDto);

                return CreatedAtAction(nameof(Get), new { id = createdProductDto.Id }, createdProductDto);
            }
            catch (ItemExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

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
        }
    }
}
