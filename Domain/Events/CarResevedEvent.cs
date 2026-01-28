namespace Domain.Events
{
    public class CarResevedEvent
    {
        public Guid CarId { get; }
        public Guid ReservationId { get; }
        public DateTime OccurredOn { get; }


        public CarResevedEvent(Guid carId, Guid reservationId)
        {
            CarId = carId;
            ReservationId = reservationId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
