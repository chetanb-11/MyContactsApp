using Microsoft.AspNetCore.Mvc;
using MyContactsApp.Core.Models;

namespace MyContactsApp.Web.Controllers;

// [ApiController]
public class ContactController : Controller
{
    private List<Contact> _contacts = [];

    public IActionResult AddContact()
    {
        var model = new Contact();
        return View(model);
    }
    
    [HttpPost]
    public IActionResult AddContact(Contact contact)
    {
        _contacts.Add(contact);
        TempData["Message"] = contact.ToString();
        return RedirectToAction("Index", "Home");
    }
}
