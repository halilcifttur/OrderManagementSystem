using FluentAssertions;
using Moq;
using OrderManagementSystem.Application.Orders.Queries;
using OrderManagementSystem.Domain.Entitites;
using OrderManagementSystem.Domain.Repositories;

namespace OrderManagementSystem.Tests.Handlers;

public class GetOrdersQueryHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly GetOrdersQueryHandler _handler;

    public GetOrdersQueryHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _handler = new GetOrdersQueryHandler(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllOrders()
    {
        // Arrange
        var orders = new List<Order>
        {
            new Order { Id = Guid.NewGuid(), ProductName = "Product 1", Price = 100 },
            new Order { Id = Guid.NewGuid(), ProductName = "Product 2", Price = 200 }
        };

        _orderRepositoryMock
            .Setup(repo => repo.GetOrdersAsync())
            .ReturnsAsync(orders);

        // Act
        var result = await _handler.Handle(new GetOrdersQuery(), CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        result.First().ProductName.Should().Be("Product 1");

        _orderRepositoryMock.Verify(repo => repo.GetOrdersAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowEntityNotFoundException_WhenNoOrdersFound()
    {
        // Arrange
        _orderRepositoryMock
            .Setup(repo => repo.GetOrdersAsync())
            .ReturnsAsync(new List<Order>());

        // Act
        Func<Task> act = async () => await _handler.Handle(new GetOrdersQuery(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}