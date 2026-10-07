using Microsoft.EntityFrameworkCore;
using MyContactsApp.Core.Models;

namespace MyContactsApp.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts => Set<Contact>();
    // public DbSet<AddressBook> AddressBooks => Set<AddressBook>(); // UC6+
}