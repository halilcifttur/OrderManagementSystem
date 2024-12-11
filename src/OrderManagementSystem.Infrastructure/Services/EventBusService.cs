using MassTransit;
using OrderManagementSystem.Domain.Services;

namespace OrderManagementSystem.Infrastructure.Services;

public class EventBusService : IEventBusService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBusService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        return _publishEndpoint.Publish(message, cancellationToken);
    }
}