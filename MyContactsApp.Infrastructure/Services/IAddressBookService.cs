using MyContactsApp.Core.Models;

namespace MyContactsApp.Infrastructure.Services;

public interface IAddressBookService
{
    Task<List<AddressBook>> GetAllAddressBooksWithContactsAsync();
    Task<int> GetTotalContactsCountAsync();
    Task<int> GetContactsCountForAddressBookAsync(int addressBookId);
}
