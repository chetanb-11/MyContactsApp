namespace MyContactsApp.Core.Models;

public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public int? AddressBookId { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName} | {Address}, " +
               $"{City}, {State} {Zip} | {PhoneNumber} | {Email}";
    }
}
