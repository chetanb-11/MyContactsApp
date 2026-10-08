using Microsoft.EntityFrameworkCore;
using MyContactsApp.Core.Models;
using MyContactsApp.Infrastructure.Data;

namespace MyContactsApp.Infrastructure.Services;

public class ContactService(AppDbContext appDbContext) : IContactService
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<Contact> CreateContact(Contact contact)
    {
        try
        {
            _appDbContext.Contacts.Add(contact);
            await _appDbContext.SaveChangesAsync();
            return contact;
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
            var exist = ExistsById(id);
            if (!exist.Result)
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