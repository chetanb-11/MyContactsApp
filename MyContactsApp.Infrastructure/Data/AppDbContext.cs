using Microsoft.EntityFrameworkCore;
using MyContactsApp.Core.Models;

namespace MyContactsApp.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<AddressBook> AddressBooks => Set<AddressBook>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AddressBook>()
            .HasMany(a => a.Contacts)
            .WithOne(c => c.AddressBook)
            .HasForeignKey(c => c.AddressBookId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
