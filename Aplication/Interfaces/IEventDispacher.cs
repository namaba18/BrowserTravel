namespace Aplication.Interfaces
{
    public interface IEventDispacher
    {
        Task DispatchAsync(object domainEvent);
    }
}
