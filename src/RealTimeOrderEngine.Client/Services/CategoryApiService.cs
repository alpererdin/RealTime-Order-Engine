using System.Net.Http.Json;
using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace RealTimeOrderEngine.Client.Services;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CategoryApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public CategoryApiService(HttpClient httpClient, ILocalStorageService localStorage)
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

    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CategoryDto>>("api/categories") ?? new();
    }

    public async Task<bool> CreateCategoryAsync(string name)
    {
        await SetAuthHeader();
        var response = await _httpClient.PostAsJsonAsync("api/categories", new { Name = name });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        await SetAuthHeader();
        var response = await _httpClient.DeleteAsync($"api/categories/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCategoryAsync(Guid id, string name)
    {
        await SetAuthHeader();
        var response = await _httpClient.PutAsJsonAsync($"api/categories/{id}", new { Name = name });
        return response.IsSuccessStatusCode;
    }
}