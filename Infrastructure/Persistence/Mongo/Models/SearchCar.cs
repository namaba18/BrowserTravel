namespace Infrastructure.Persistence.Mongo.Models
{
    public class SearchCar
    {
        public string Id { get; set; }
        public Guid LocationId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<CarResult> Cars { get; set; } = new();        
    }

    public class CarResult
    {
        public Guid CarId { get; set; }
        public string Model { get; set; } = default!;
        public decimal PricePerDay { get; set; }
    }
}
