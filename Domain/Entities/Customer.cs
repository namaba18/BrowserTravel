using Domain.Common;

namespace Domain.Entities
{
    public class Customer : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DriverLicenseNumber { get; set; }

        private Customer()
        {

        }

        public Customer(string firstName, string lastName, string email, string phoneNumber, string driverLicenseNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DriverLicenseNumber = driverLicenseNumber;
        }
    }
}
