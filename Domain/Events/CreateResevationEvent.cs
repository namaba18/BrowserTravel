namespace Domain.Events
{
    public class CreateResevationEvent
    {
        public Guid CarId { get; }
        public Guid ReservationId { get; }
        public DateTime OccurredOn { get; }


        public CreateResevationEvent(Guid carId, Guid reservationId)
        {
            CarId = carId;
            ReservationId = reservationId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
