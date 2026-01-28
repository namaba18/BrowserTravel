namespace Aplication.DTOs
{
    public class CreateReservationCommand
    {
        public Guid CarId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid PickUpLocationId { get; set; }
        public Guid DropOffLocationId { get; set; }
        public string? Note { get; set; }
    }
}
