namespace Domain.Entities
{
    public enum CarType
    {
        Sedan,
        SUV,
        Coupe,
        Hatchback,
        Convertible,
        Truck,
        Van
    }

    public enum FuelType
    {
        Gasoline,
        Diesel,
        Electric,
        Hybrid,
        Hydrogen
    }
    public enum Color
    {
        Red,
        Blue,
        Green,
        Black,
        White,
        Silver,
        Yellow
    }

    public enum TransmissionType
    {
        Manual,
        Automatic,
        SemiAutomatic
    }

    public enum CarStatus
    {
        Available,
        Rented,
        Maintenance,
        Reserved
    }

    public class Car : EntityBase
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public CarType Type { get; set; }
        public FuelType Fuel { get; set; }
        public Color Color { get; set; }
        public TransmissionType Transmission { get; set; }
        public CarStatus Status { get; set; }
        public Location Location { get; set; }

        public Car()
        {
            
        }

    }
}
