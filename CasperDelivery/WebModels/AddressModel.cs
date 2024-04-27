using Microsoft.Build.Framework;

namespace CasperDelivery.WebModels;

public class AddressModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string PhoneNumber { get; set; }

}