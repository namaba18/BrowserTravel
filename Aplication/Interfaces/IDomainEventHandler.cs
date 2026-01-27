namespace Aplication.Interfaces
{
    public interface IDomainEventHandler<TEvent>
    {
        Task Handle(TEvent domainEvent);
    }
}
