using MyApi.DTOs;
using MyApi.Models;
using MyApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Services {
    public class ProductService {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync() {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto {
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity
            });
        }
    }
}
