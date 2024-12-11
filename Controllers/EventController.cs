using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using mvc.Models;
using System.Linq;

[Authorize]
public class EventController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Liste des événements (accessible par tous les utilisateurs authentifiés)
    [AllowAnonymous]
    public IActionResult Index()
    {
        var events = _context.Events.ToList();
        return View(events);
    }

    // Détails d'un événement (accessible par tous les utilisateurs authentifiés)
    [AllowAnonymous]
    public IActionResult Details(int id)
    {
        var ev = _context.Events.Find(id);
        if (ev == null)
        {
            return NotFound();
        }
        return View(ev);
    }

    // Créer un événement (réservé aux Teachers)
    [Authorize(Roles = "Teacher")]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public IActionResult Create(Event ev)
    {
        if (ModelState.IsValid)
        {
            _context.Events.Add(ev);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(ev);
    }

    // Modifier un événement (réservé aux Teachers)
    [Authorize(Roles = "Teacher")]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var ev = _context.Events.Find(id);
        if (ev == null)
        {
            return NotFound();
        }
        return View(ev);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public IActionResult Edit(Event ev)
    {
        if (ModelState.IsValid)
        {
            _context.Events.Update(ev);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(ev);
    }

    // Supprimer un événement (réservé aux Teachers)
    [Authorize(Roles = "Teacher")]
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var ev = _context.Events.Find(id);
        if (ev == null)
        {
            return NotFound();
        }
        return View(ev);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var ev = _context.Events.Find(id);
        if (ev != null)
        {
            _context.Events.Remove(ev);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}
