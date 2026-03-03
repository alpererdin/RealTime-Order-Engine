using System.Net.Http.Json;
using RealTimeOrderEngine.Shared.DTOs.Reviews;

namespace RealTimeOrderEngine.Client.Services;

public class ReviewApiService
{
    private readonly HttpClient _httpClient;

    public ReviewApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(Guid productId)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ReviewDto>>($"api/reviews/product/{productId}") 
               ?? Enumerable.Empty<ReviewDto>();
    }

    public async Task CreateReviewAsync(CreateReviewDto dto)
    {
        await _httpClient.PostAsJsonAsync("api/reviews", dto);
    }
}