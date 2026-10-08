namespace MyContactsApp.Core.Models;

public class AddressBook
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<Contact> Contacts { get; set; } = [];
}
