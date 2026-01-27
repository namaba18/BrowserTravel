using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Reservation : EntityBase
    {
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
        }
    }
}
