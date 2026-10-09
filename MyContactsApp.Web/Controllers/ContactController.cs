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

    public async Task<IActionResult> Index(string? searchQuery)
    {
        if (string.IsNullOrEmpty(searchQuery))
        {
            return View();
        }
        var contacts = await _contactService.SearchContacts(searchQuery);
        return View(contacts);
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

        if (await _contactService.ExistsByName(contact.FirstName, contact.LastName))
        {
            ModelState.AddModelError(string.Empty, "Contact with same name already exists");
            return View(contact);
        }

        if (await _contactService.ExistsByEmail(contact.Email))
        {
            ModelState.AddModelError(nameof(contact.Email), "A contact with this email already exists.");
            return View(contact);
        }

        var createdContact = await _contactService.CreateContact(contact);
        if (createdContact == null)
        {
            ModelState.AddModelError(string.Empty, "Unable to save contact. A contact with this name or email may already exist.");
            return View(contact);
        }

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

        if (await _contactService.ExistsByEmail(updatedContact.Email, id))
        {
            ModelState.AddModelError(nameof(updatedContact.Email), "A contact with this email already exists.");
            return View(updatedContact);
        }

        var contact = await _contactService.UpdateContact(id, updatedContact);
        if (contact == null)
        {
            ModelState.AddModelError(string.Empty, "Unable to update contact.");
            return View(updatedContact);
        }

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
    public async Task<IActionResult> ShowAll(string? sortBy, bool groupByState = false, bool groupByCity = false)
    {
        var contacts = await _contactService.ShowAllContacts();

        if (sortBy == "name")
        {
            contacts = contacts
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ToList();
        }
        ViewBag.TotalContacts = await _addressBookService.GetTotalContactsCountAsync();
        ViewBag.GroupByState = groupByState;
        ViewBag.GroupByCity = groupByCity;
        
        return View(contacts);
    }
}
