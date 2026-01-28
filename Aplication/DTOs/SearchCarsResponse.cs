namespace Aplication.DTOs
{
    public class SearchCarsResponse
    {
        public Guid Id { get; set; }
        public string Plate { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public string Type { get; set; } = default!;
        public string Fuel { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string Transmission { get; set; } = default!;
        public string Status { get; set; } = default!;

    }
}
