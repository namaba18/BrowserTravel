using Aplication.Interfaces;
using Domain.Events;

namespace Infrastructure.Events.Handlers
{
    public class CarReservedEventHandler : IDomainEventHandler<CarResevedEvent>
    {
        public Task Handle(CarResevedEvent domainEvent)
        {
            Console.WriteLine(
                $"Vehicle {domainEvent.CarId} reserved");

            return Task.CompletedTask;
        }
    }
}
