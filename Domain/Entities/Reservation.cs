using Domain.Common;
using Domain.Events;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Reservation : EntityBase
    {
        private readonly List<object> _domainEvents = new();
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        public Car Car { get; set; }
        public Customer Customer { get; set; }
        public DateRange DateRange { get; set; }
        public Location PickUpLocation { get; set; }
        public Location DropOffLocation { get; set; }
        public string? Note { get; set; }

        public Reservation()
        {
            
        }

        public Reservation(Car car, Customer customer, DateRange dateRange, Location PickUp, Location DropOff)
        {
            Car = car;
            Customer = customer;
            DateRange = dateRange;
            PickUpLocation = PickUp;
            DropOffLocation = DropOff;

            AddDomainEvent(new CarResevedEvent(car.Id, Id));
        }

        private void AddDomainEvent(object domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
