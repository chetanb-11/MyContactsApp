using Microsoft.EntityFrameworkCore;
using MyContactsApp.Core.Models;
using MyContactsApp.Infrastructure.Data;

namespace MyContactsApp.Infrastructure.Services;

public class AddressBookService(AppDbContext dbContext) : IAddressBookService
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<List<AddressBook>> GetAllAddressBooksWithContactsAsync()
    {
        return await _dbContext.AddressBooks
            .Include(a => a.Contacts)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> GetTotalContactsCountAsync()
    {
        return await _dbContext.Contacts.CountAsync();
    }

    public async Task<int> GetContactsCountForAddressBookAsync(int addressBookId)
    {
        var addressBook = await _dbContext.AddressBooks
            .Include(a => a.Contacts)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == addressBookId);

        return addressBook?.Contacts.Count ?? 0;
    }
}
