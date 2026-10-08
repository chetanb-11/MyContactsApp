using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContactsApp.Core.Models;
using MyContactsApp.Infrastructure.Data;

namespace MyContactsApp.Web.Controllers;

public class ContactController(AppDbContext appDbContext) : Controller
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AddContact()
    {
        var model = new Contact();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddContact(Contact contact)
    {
        if (!ModelState.IsValid)
        {
            return View(contact);
        }

        _appDbContext.Contacts.Add(contact);
        await _appDbContext.SaveChangesAsync();
        TempData["Message"] = $"Contact '{contact.FirstName} {contact.LastName}' was successfully added!";
        return RedirectToAction(nameof(ShowAllContacts));
    }

    [HttpGet]
    public async Task<IActionResult> EditContact(int id)
    {
        var contact = await _appDbContext.Contacts.FindAsync(id);
        if (contact == null)
        {
            return NotFound();
        }
        return View(contact);
    }

    [HttpPost]
    public async Task<IActionResult> EditContact(int id, Contact contact)
    {
        if (id != contact.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(contact);
        }

        try
        {
            _appDbContext.Contacts.Update(contact);
            await _appDbContext.SaveChangesAsync();
            TempData["Message"] = $"Contact '{contact.FirstName} {contact.LastName}' was successfully updated!";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _appDbContext.Contacts.AnyAsync(c => c.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToAction(nameof(ShowAllContacts));
    }

    [HttpGet]
    public async Task<IActionResult> ShowAllContacts()
    {
        var contacts = await _appDbContext.Contacts.AsNoTracking().ToListAsync();
        return View(contacts);
    }
}
