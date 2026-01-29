using Domain.Entities;

namespace Domain.Models
{
    public class SearchCar
    {
        public string Id { get; set; }
        public string LocationId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<CarResult> Cars { get; set; } = new();        
    }

    public class CarResult
    {
        public string CarId { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public string Fuel { get; set; }
        public string Color { get; set; }
        public string Transmission { get; set; }
        public string Status { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
