namespace MyContactsApp.Core.Models;

using System.ComponentModel.DataAnnotations;

public class Contact
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [RegularExpression(@"^[A-Z][a-zA-Z]{2,}$", ErrorMessage = "First name must start with a capital letter and have at least 3 characters.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [RegularExpression(@"^[A-Z][a-zA-Z]{2,}$", ErrorMessage = "Last name must start with a capital letter and have at least 3 characters.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required.")]
    [RegularExpression(@"^.{4,}$", ErrorMessage = "Address must be at least 4 characters.")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required.")]
    [RegularExpression(@"^.{4,}$", ErrorMessage = "City must be at least 4 characters.")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "State is required.")]
    [RegularExpression(@"^.{4,}$", ErrorMessage = "State must be at least 4 characters.")]
    public string State { get; set; } = string.Empty;

    [Required(ErrorMessage = "Zip code is required.")]
    [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "Zip code must be exactly 6 digits.")]
    public string Zip { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    public int? AddressBookId { get; set; }
    public AddressBook? AddressBook { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName} | {Address}, " +
               $"{City}, {State} {Zip} | {PhoneNumber} | {Email}";
    }
}
