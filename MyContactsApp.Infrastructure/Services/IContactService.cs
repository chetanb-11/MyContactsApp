using MyContactsApp.Core.Models;

namespace MyContactsApp.Infrastructure.Services;

public interface IContactService
{
    public Task<Contact?> CreateContact(Contact contact);
    public Task<bool> ExistsByContact(Contact contact);
    public Task<bool> ExistsByName(string firstName, string lastName);
    public Task<bool> ExistsByEmail(string email, int? excludeId = null);
    public Task<bool> ExistsById(int id);
    public Task<Contact?> FindContact(int id);
    public Task<Contact?> UpdateContact(int id, Contact contact);
    public Task<bool> DeleteContact(int id);
    public Task<List<Contact>> ShowAllContacts();
    public Task<List<Contact>> SearchContacts(string searchQuery);
}
