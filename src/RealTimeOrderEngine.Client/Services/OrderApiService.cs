using System.Net.Http.Json;
using RealTimeOrderEngine.Shared.DTOs.Orders;

namespace RealTimeOrderEngine.Client.Services;

public class OrderApiService
{
    private readonly HttpClient _httpClient;

    public OrderApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<OrderDto>>("api/orders") ?? new List<OrderDto>();
    }

    public async Task<OrderDto?> CreateOrderAsync(CreateOrderDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/orders", dto);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<OrderDto>();
        }
        return null;
    }

    public async Task<bool> UpdateOrderStatusAsync(Guid orderId, int status)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/orders/{orderId}/status", status);
        return response.IsSuccessStatusCode;
    }
}