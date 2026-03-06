using System.Net.Http.Json;
using RealTimeOrderEngine.Shared.DTOs.Products;

namespace RealTimeOrderEngine.Client.Services;

public class ProductApiService
{
    private readonly HttpClient _httpClient;

    public ProductApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/products") ?? new List<ProductDto>();
    }

    public async Task<bool> CreateProductAsync(CreateProductDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/products", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateProductAsync(Guid id, UpdateProductDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/products/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/products/{id}");
        return response.IsSuccessStatusCode;
    }
}