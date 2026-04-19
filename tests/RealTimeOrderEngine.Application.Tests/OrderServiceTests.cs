using Moq;
using RealTimeOrderEngine.Application.Exceptions;
using RealTimeOrderEngine.Application.Interfaces.Repositories;
using RealTimeOrderEngine.Application.Interfaces.Services;
using RealTimeOrderEngine.Application.Services;
using RealTimeOrderEngine.Domain.Entities;
using RealTimeOrderEngine.Domain.Enums;
using RealTimeOrderEngine.Shared.Contracts;
using RealTimeOrderEngine.Shared.DTOs.Orders;
using Xunit;

namespace RealTimeOrderEngine.Application.Tests;

public class OrderServiceTests
{
    [Fact]
    public async Task CreateOrderAsync_DeductsTrackedStock_AndPublishesNotification()
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Burger",
            Price = 15,
            CategoryId = Guid.NewGuid(),
            Category = new Category { Name = "Main" },
            StockQuantity = 10,
            IsStockTracked = true,
            IsAvailable = true
        };

        var orderRepository = new Mock<IOrderRepository>();
        var notificationService = new Mock<IOrderNotificationService>();
        var productRepository = new Mock<IProductRepository>();

        productRepository.Setup(x => x.GetByIdAsync(product.Id)).ReturnsAsync(product);
        productRepository.Setup(x => x.UpdateAsync(product)).Returns(Task.CompletedTask);
        orderRepository
            .Setup(x => x.AddAsync(It.IsAny<Order>()))
            .ReturnsAsync((Order order) =>
            {
                order.Id = Guid.NewGuid();
                order.Table = new Table { Id = order.TableId, TableNumber = "A1" };
                return order;
            });

        var sut = new OrderService(orderRepository.Object, notificationService.Object, productRepository.Object);

        var result = await sut.CreateOrderAsync(new CreateOrderDto
        {
            TableId = Guid.NewGuid(),
            SessionId = Guid.NewGuid(),
            Items =
            [
                new CreateOrderItemDto
                {
                    ProductId = product.Id,
                    Quantity = 3,
                    Note = "No onions"
                }
            ]
        });

        Assert.Equal(7, product.StockQuantity);
        Assert.Equal(45, result.TotalAmount);
        Assert.Equal("A1", result.TableNumber);
        productRepository.Verify(x => x.UpdateAsync(product), Times.Once);
        notificationService.Verify(
            x => x.NotifyOrderCreatedAsync(It.Is<OrderCreatedMessage>(m => m.OrderId == result.Id)),
            Times.Once);
    }

    [Fact]
    public async Task CreateOrderAsync_ThrowsBusinessRuleException_WhenTrackedStockIsInsufficient()
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Burger",
            Price = 15,
            CategoryId = Guid.NewGuid(),
            Category = new Category { Name = "Main" },
            StockQuantity = 1,
            IsStockTracked = true,
            IsAvailable = true
        };

        var orderRepository = new Mock<IOrderRepository>();
        var notificationService = new Mock<IOrderNotificationService>();
        var productRepository = new Mock<IProductRepository>();

        productRepository.Setup(x => x.GetByIdAsync(product.Id)).ReturnsAsync(product);

        var sut = new OrderService(orderRepository.Object, notificationService.Object, productRepository.Object);

        await Assert.ThrowsAsync<BusinessRuleException>(() => sut.CreateOrderAsync(new CreateOrderDto
        {
            TableId = Guid.NewGuid(),
            Items =
            [
                new CreateOrderItemDto
                {
                    ProductId = product.Id,
                    Quantity = 2
                }
            ]
        }));

        orderRepository.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Never);
        notificationService.Verify(x => x.NotifyOrderCreatedAsync(It.IsAny<OrderCreatedMessage>()), Times.Never);
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_ReturnsTrue_WhenOrderExists()
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            TableId = Guid.NewGuid(),
            Table = new Table { TableNumber = "A1" },
            Status = OrderStatus.Pending
        };

        var orderRepository = new Mock<IOrderRepository>();
        var notificationService = new Mock<IOrderNotificationService>();
        var productRepository = new Mock<IProductRepository>();

        orderRepository.Setup(x => x.GetByIdAsync(order.Id)).ReturnsAsync(order);
        orderRepository.Setup(x => x.UpdateAsync(order)).Returns(Task.CompletedTask);

        var sut = new OrderService(orderRepository.Object, notificationService.Object, productRepository.Object);

        var updated = await sut.UpdateOrderStatusAsync(order.Id, OrderStatus.Ready);

        Assert.True(updated);
        Assert.Equal(OrderStatus.Ready, order.Status);
        notificationService.Verify(
            x => x.NotifyOrderStatusChangedAsync(It.Is<OrderStatusChangedMessage>(m => m.OrderId == order.Id && m.NewStatus == "Ready")),
            Times.Once);
    }
}
