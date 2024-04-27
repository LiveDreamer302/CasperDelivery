namespace CasperDelivery.Data.Models;

public class Address : BaseEntity
{
    
    public Address()
    {
    }

    public Address(string firstName, string lastName,
        string street, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        PhoneNumber = phoneNumber;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string PhoneNumber { get; set; }
    public AppUser User { get; set; }
}