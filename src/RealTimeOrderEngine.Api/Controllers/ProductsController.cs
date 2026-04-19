using Microsoft.AspNetCore.Mvc;
using RealTimeOrderEngine.Application.Services;
using RealTimeOrderEngine.Shared.DTOs.Products;
using RealTimeOrderEngine.Shared.DTOs.Stock;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize(Roles = "Admin")]
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

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto dto)
    {
        var updated = await _productService.UpdateProductAsync(id, dto);
        if (!updated) return NotFound();
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        var deleted = await _productService.DeleteProductAsync(id);
        if (!deleted) return NotFound();
        return Ok();
    }

    [HttpPatch("stock")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateStock([FromBody] UpdateStockDto dto)
    {
        var result = await _productService.UpdateStockAsync(dto);
        if (!result) return NotFound();
        return Ok();
    }
}
