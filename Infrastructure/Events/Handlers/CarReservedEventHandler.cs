using Aplication.Interfaces;
using Domain.Events;

namespace Infrastructure.Events.Handlers
{
    public class CarReservedEventHandler : IDomainEventHandler<CreateResevationEvent>
    {
        public Task Handle(CreateResevationEvent domainEvent)
        {
            Console.WriteLine(
                $"Vehicle {domainEvent.CarId} reserved");

            return Task.CompletedTask;
        }
    }
}
