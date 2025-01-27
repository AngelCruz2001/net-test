using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Repositories.Interfaces;
using MyApi.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using MyApi.Utils;
using Microsoft.Extensions.Logging;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            // All Products
            LoggerHelper.LogInfo(_logger, "Fetching all products.");

            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            // pRODUCTS BY ID
            LoggerHelper.LogInfo(_logger, $"Fetching product with id {id}.");
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity
            };
            
            // Add new product
            LoggerHelper.LogInfo(_logger, "Adding new product.");
            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Product product)
        {

            LoggerHelper.LogInfo(_logger, $"Updating product with id {id}.");
            if (id != product.Id)
            {
                LoggerHelper.LogError(_logger, "Product id mismatch.");
                return BadRequest();
            }

            await _productRepository.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            LoggerHelper.LogInfo(_logger, $"Deleting product with id {id}.");
            if (existingProduct == null)
            {
                LoggerHelper.LogError(_logger, "Product not found.");
                return NotFound();
            }

            await _productRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
