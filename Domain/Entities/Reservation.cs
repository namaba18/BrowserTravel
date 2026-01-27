namespace Domain.Entities
{
    public class Reservation : EntityBase
    {
        public Car Car { get; set; }
        public Customer Customer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Location PickUpLocation { get; set; }
        public Location DropOffLocation { get; set; }
        public string Note { get; set; }
    }
}
