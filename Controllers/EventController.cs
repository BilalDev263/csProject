using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using mvc.Models;
using System.Linq;

public class EventController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Liste des événements
    public IActionResult Index()
    {
        var events = _context.Events.ToList();
        return View(events);
    }

    // Détails d'un événement
    public IActionResult Details(int id)
    {
        var ev = _context.Events.Find(id);
        if (ev == null)
        {
            return NotFound();
        }
        return View(ev);
    }

    // Afficher le formulaire de création
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Traiter le formulaire de création
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

    // Afficher le formulaire d'édition
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

    // Traiter le formulaire d'édition
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

    // Supprimer un événement
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
