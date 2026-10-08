using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContactsApp.Core.Models;
using MyContactsApp.Infrastructure.Data;
using MyContactsApp.Infrastructure.Services;

namespace MyContactsApp.Web.Controllers;

public class ContactController(
    IAddressBookService addressBookService,
    IContactService contactService) : Controller
{
    private readonly IAddressBookService _addressBookService = addressBookService;
    private readonly IContactService _contactService = contactService;

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Add()
    {
        var model = new Contact();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Contact contact)
    {
        if (!ModelState.IsValid)
        {
            return View(contact);
        }

        var createdContact = await _contactService.CreateContact(contact);

        TempData["Message"] =
            $"Contact '{createdContact.FirstName} {createdContact.LastName}' was successfully added!";
        return RedirectToAction(nameof(ShowAll));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var contact = await _contactService.FindContact(id);
        if (contact == null)
        {
            return NotFound();
        }

        return View(contact);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Contact updatedContact)
    {
        if (id != updatedContact.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(updatedContact);
        }

        var contact = await _contactService.UpdateContact(id, updatedContact);
        if (contact != null)
            TempData["Message"] = $"Contact '{contact.FirstName} {contact.LastName}' was successfully updated!";

        return RedirectToAction(nameof(ShowAll));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _contactService.DeleteContact(id);
        
        return deleted ? RedirectToAction(nameof(ShowAll)) : NotFound("Contact not found");
    }

    [HttpGet]
    public async Task<IActionResult> ShowAll()
    {
        var contacts = await _contactService.ShowAllContacts();

        ViewBag.TotalContacts = await _addressBookService.GetTotalContactsCountAsync();
        return View(contacts);
    }
}