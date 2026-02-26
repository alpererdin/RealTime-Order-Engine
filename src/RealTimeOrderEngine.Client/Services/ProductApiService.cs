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
}