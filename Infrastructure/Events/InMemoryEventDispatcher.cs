using Aplication.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Events
{
    public class InMemoryEventDispatcher : IEventDispacher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync(object domainEvent)
        {
            var handlerType = typeof(IDomainEventHandler<>)
                .MakeGenericType(domainEvent.GetType());

            var handlers = _serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                var method = handlerType.GetMethod("Handle");
                await (Task)method!.Invoke(handler, new[] { domainEvent })!;
            }
        }
    }
}
