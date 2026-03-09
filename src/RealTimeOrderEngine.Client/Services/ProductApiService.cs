using System.Net.Http.Json;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using RealTimeOrderEngine.Shared.DTOs.Products;
using RealTimeOrderEngine.Shared.DTOs.Stock;

namespace RealTimeOrderEngine.Client.Services;

public class ProductApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public ProductApiService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    private async Task SetAuthHeader()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrWhiteSpace(token))
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/products") ?? new List<ProductDto>();
    }

    public async Task<bool> CreateProductAsync(CreateProductDto dto)
    {
        await SetAuthHeader();
        var response = await _httpClient.PostAsJsonAsync("api/products", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateProductAsync(Guid id, UpdateProductDto dto)
    {
        await SetAuthHeader();
        var response = await _httpClient.PutAsJsonAsync($"api/products/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        await SetAuthHeader();
        var response = await _httpClient.DeleteAsync($"api/products/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateStockAsync(UpdateStockDto dto)
    {
        await SetAuthHeader();
        var response = await _httpClient.PatchAsJsonAsync("api/products/stock", dto);
        return response.IsSuccessStatusCode;
    }
}