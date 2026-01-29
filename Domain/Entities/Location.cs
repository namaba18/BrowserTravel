using Domain.Common;

namespace Domain.Entities
{
    public class Location : EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }


        private Location() { }

        public Location(string country, string state, string city, string address, string name)
        {
            Country = country;
            State = state;
            City = city;
            Address = address;
            Name = name;
        }
    }
}
