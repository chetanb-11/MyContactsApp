using Microsoft.EntityFrameworkCore;
using MyContactsApp.Core.Models;
using MyContactsApp.Infrastructure.Data;

namespace MyContactsApp.Infrastructure.Services;

public class ContactService(AppDbContext appDbContext) : IContactService
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public Task<List<Contact>> SearchContacts(string searchQuery)
    {
        try
        {
            var term = searchQuery;
            var query = _appDbContext.Contacts
                .AsQueryable()
                .Include(c => c.AddressBook)
                .AsNoTracking();

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return query
                    .OrderBy(c => c.Id)
                    .ToListAsync();
            }

            var searchContacts = query.Where(c =>
                c.FirstName.Contains(term) ||
                c.LastName.Contains(term) ||
                c.Email.Contains(term) ||
                c.PhoneNumber.Contains(term) ||
                c.City.Contains(term)
            );
            return searchContacts.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Contact?> CreateContact(Contact contact)
    {
        try
        {
            var exists = await ExistsByName(contact.FirstName, contact.LastName);
            if (exists)
            {
                return null;
            }

            var emailExists = await ExistsByEmail(contact.Email);
            if (emailExists)
            {
                return null;
            }

            _appDbContext.Contacts.Add(contact);
            await _appDbContext.SaveChangesAsync();
            return contact;
        }
        catch (DbUpdateException)
        {
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> ExistsByContact(Contact contact)
    {
        return await ExistsById(contact.Id);
    }

    public Task<bool> ExistsByName(string firstName, string lastName)
    {
        return _appDbContext.Contacts.AnyAsync(c => c.FirstName == firstName && c.LastName == lastName);
    }

    public Task<bool> ExistsByEmail(string email, int? excludeId = null)
    {
        return _appDbContext.Contacts
            .AnyAsync(c => c.Email == email && (!excludeId.HasValue || c.Id != excludeId.Value));
    }

    public async Task<bool> ExistsById(int id)
    {
        try
        {
            var contact = await _appDbContext.Contacts.FindAsync(id);
            return contact != null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Contact?> FindContact(int id)
    {
        try
        {
            var exist = await ExistsById(id);
            if (!exist)
            {
                return null;
            }

            var contact = await _appDbContext.Contacts.FindAsync(id);
            return contact;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Contact?> UpdateContact(int id, Contact contact)
    {
        try
        {
            var emailExists = await ExistsByEmail(contact.Email, id);
            if (emailExists)
            {
                return null;
            }

            _appDbContext.Contacts.Update(contact);
            await _appDbContext.SaveChangesAsync();
            return contact;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _appDbContext.Contacts.AnyAsync(c => c.Id == id))
            {
                return null;
            }

            throw;
        }
        catch (DbUpdateException)
        {
            return null;
        }
    }

    public async Task<bool> DeleteContact(int id)
    {
        try
        {
            var affectedRows = await _appDbContext.Contacts.Where(c => c.Id == id).ExecuteDeleteAsync();
            return affectedRows != 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Contact>> ShowAllContacts()
    {
        try
        {
            var contacts = await _appDbContext.Contacts
                .Include(c => c.AddressBook)
                .AsNoTracking()
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            return contacts;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
