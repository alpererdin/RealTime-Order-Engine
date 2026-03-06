using System.Net.Http.Json;
using RealTimeOrderEngine.Shared.DTOs.Staff;

namespace RealTimeOrderEngine.Client.Services;

public class StaffApiService
{
    private readonly HttpClient _httpClient;

    public StaffApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<StaffDto>> GetStaffAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<StaffDto>>("api/staff") ?? new List<StaffDto>();
    }

    public async Task<bool> CreateStaffAsync(CreateStaffDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/staff", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteStaffAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/staff/{id}");
        return response.IsSuccessStatusCode;
    }
}