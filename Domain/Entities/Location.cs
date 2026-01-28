using Domain.Common;

namespace Domain.Entities
{
    public class Location : EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        

        private Location() { }

        public Location(string state, string city, string address)
        {
            State = state;
            City = city;
            Address = address;
        }
    }
}
