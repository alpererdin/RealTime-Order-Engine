using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTimeOrderEngine.Api.Hubs;
using RealTimeOrderEngine.Application.Interfaces.Services;
using RealTimeOrderEngine.Shared.DTOs.Reviews;

namespace RealTimeOrderEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IHubContext<OrderHub> _hubContext;

    public ReviewsController(IReviewService reviewService, IHubContext<OrderHub> hubContext)
    {
        _reviewService = reviewService;
        _hubContext = hubContext;
    }

    [HttpPost]
    public async Task<ActionResult<ReviewDto>> CreateReview(CreateReviewDto dto)
    {
        try
        {
            var result = await _reviewService.CreateReviewAsync(dto);
            await _hubContext.Clients.All.SendAsync("ReviewSubmitted", dto.OrderId, dto.ProductId);
            return CreatedAtAction(nameof(GetReviewsByProduct), new { productId = result.ProductId }, result);
        }
        catch (InvalidOperationException)
        {
            return Conflict(new { message = "Already reviewed" });
        }
    }

    [HttpGet("product/{productId}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByProduct(Guid productId)
    {
        var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);
        return Ok(reviews);
    }
}