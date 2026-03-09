using RealTimeOrderEngine.Application.Interfaces.Repositories;
using RealTimeOrderEngine.Domain.Entities;
using RealTimeOrderEngine.Shared.DTOs.Products;
using RealTimeOrderEngine.Shared.DTOs.Stock;

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
            Description = dto.Description,
            ImageUrl = dto.ImageUrl,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            IsAvailable = true,
            StockQuantity = dto.StockQuantity,
            IsStockTracked = dto.IsStockTracked,
            Category = null!
        };

        var createdProduct = await _productRepository.AddAsync(product);

        return new ProductDto
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Description = createdProduct.Description,   
            ImageUrl = createdProduct.ImageUrl, 
            Price = createdProduct.Price,
            CategoryId = createdProduct.CategoryId,
            IsAvailable = createdProduct.IsAvailable,
            StockQuantity = createdProduct.StockQuantity,
            IsStockTracked = createdProduct.IsStockTracked,
            AverageRating = 0,
            ReviewCount = 0
        };
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,  
            ImageUrl = p.ImageUrl,   
            Price = p.Price,
            CategoryId = p.CategoryId,
            IsAvailable = p.IsAvailable,
            StockQuantity = p.StockQuantity,
            IsStockTracked = p.IsStockTracked,
            AverageRating = p.Reviews != null && p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0,
            ReviewCount = p.Reviews != null ? p.Reviews.Count : 0
        });
    }

    public async Task<bool> UpdateStockAsync(UpdateStockDto dto)
    {
        var product = await _productRepository.GetByIdAsync(dto.ProductId);
        if (product == null) return false;

        product.StockQuantity = dto.StockQuantity;
        product.IsStockTracked = dto.IsStockTracked;

        await _productRepository.UpdateAsync(product);
        return true;
    }
}