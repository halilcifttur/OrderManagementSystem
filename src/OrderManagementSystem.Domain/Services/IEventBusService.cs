namespace OrderManagementSystem.Domain.Services;

public interface IEventBusService
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
}