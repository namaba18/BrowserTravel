namespace Aplication.Interfaces
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(object domainEvent);
    }
}
