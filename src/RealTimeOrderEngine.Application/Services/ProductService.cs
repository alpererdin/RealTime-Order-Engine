using RealTimeOrderEngine.Application.Interfaces.Repositories;
using RealTimeOrderEngine.Domain.Entities;
using RealTimeOrderEngine.Shared.DTOs.Products;

namespace RealTimeOrderEngine.Application.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            IsAvailable = true,
            Category = null!
        };

        var createdProduct = await _productRepository.AddAsync(product);

        return new ProductDto
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Price = createdProduct.Price,
            CategoryId = createdProduct.CategoryId,
            IsAvailable = createdProduct.IsAvailable
        };
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CategoryId = p.CategoryId,
            IsAvailable = p.IsAvailable
        });
    }
}