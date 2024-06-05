using GestaoProdutos.Domain.Dto;
using GestaoProdutos.Domain.Entidades;
using GestaoProdutos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GesataoProdutos.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ProductQueryParams queryParams)
        {
            var products = await _productService.ListAsync(queryParams);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is null.");
            }

            await _productService.AddAsync(productDto);

            // Retrieve the created product to get the generated ProductId
            var createdProduct = await _productService.GetByIdAsync(productDto.ProductId);

            return CreatedAtAction(nameof(GetById), new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.ProductId)
            {
                return BadRequest("Product ID mismatch");
            }

            try
            {
                var updateProduct =  await _productService.UpdateAsync(productDto);
                return Ok(updateProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok("Produto deletado com sucesso.");
        }
    }

}
