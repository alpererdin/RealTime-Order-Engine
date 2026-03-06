using System.Net.Http.Json;
using RealTimeOrderEngine.Shared.DTOs.Tables;

namespace RealTimeOrderEngine.Client.Services;

public class TableApiService
{
    private readonly HttpClient _httpClient;

    public TableApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<TableDto>> GetTablesAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<TableDto>>("api/tables") ?? new List<TableDto>();
    }

    public async Task<TableDto?> GetTableByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/tables/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TableDto>();
        }
        return null;
    }

    public async Task<TableDto?> OpenTableAsync(Guid id)
    {
        var response = await _httpClient.PostAsync($"api/tables/{id}/open", null);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TableDto>();
        }
        return null;
    }

    public async Task<bool> CloseTableAsync(Guid id)
    {
        var response = await _httpClient.PostAsync($"api/tables/{id}/close", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ValidateSessionAsync(Guid id, Guid sessionId)
    {
        return await _httpClient.GetFromJsonAsync<bool>($"api/tables/{id}/validate?sessionId={sessionId}");
    }
    public async Task<bool> UpdateReviewPermissionAsync(Guid id, bool isAllowed)
    {
        var response = await _httpClient.PutAsync($"api/tables/{id}/review-permission?isAllowed={isAllowed}", null);
        return response.IsSuccessStatusCode;
    }
}