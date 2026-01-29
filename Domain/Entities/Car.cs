using Domain.Common;

namespace Domain.Entities
{
    public enum CarTypeEnum
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
        Maintenance
    }

    public class Car : EntityBase
    {
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public CarTypeEnum Type { get; set; }
        public FuelType Fuel { get; set; }
        public Color Color { get; set; }
        public TransmissionType Transmission { get; set; }
        public CarStatus Status { get; set; }
        public Location Location { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        private Car()
        {

        }

        public Car(Location location, string plate, string brand, string model, int year, decimal price)
        {
            Plate=plate;
            Brand=brand;
            Model=model;
            Year=year;
            PricePerDay=price;
            Location=location;
            Type=CarTypeEnum.Sedan;
            Status=CarStatus.Available;
            Fuel=FuelType.Gasoline;
            Color=Color.Silver;
            Transmission=TransmissionType.Automatic;
        }

    }
}
