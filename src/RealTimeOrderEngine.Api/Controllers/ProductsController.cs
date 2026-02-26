using Microsoft.AspNetCore.Mvc;
using RealTimeOrderEngine.Application.Services;
using RealTimeOrderEngine.Shared.DTOs.Products;

namespace RealTimeOrderEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var product = await _productService.CreateProductAsync(dto);
        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }
}