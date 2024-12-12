using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using mvc.Models;

public class TeacherController : Controller
{
    private readonly UserManager<Teacher> _userManager;
    private readonly ApplicationDbContext _context;

    public TeacherController(UserManager<Teacher> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    // Liste des enseignants
    public IActionResult Index()
    {
        var teachers = _userManager.Users.ToList();
        return View(teachers);
    }

    // Détails d'un enseignant
    public async Task<IActionResult> Details(string id)
    {
        var teacher = await _userManager.FindByIdAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }
        return View(teacher);
    }

    // Ajouter un enseignant (GET)
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Ajouter un enseignant (POST)
    [HttpGet]
public IActionResult Add()
{
    return View();
}

[HttpPost]
public async Task<IActionResult> Add(TeacherViewModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var teacher = new Teacher
    {
        UserName = model.Email,
        Email = model.Email,
        Firstname = model.Firstname,
        Lastname = model.Lastname,
        PersonalWebSite = model.PersonalWebSite
    };

    var result = await _userManager.CreateAsync(teacher, model.Password);

    if (result.Succeeded)
    {
        await _userManager.AddToRoleAsync(teacher, "Teacher");
        return RedirectToAction(nameof(Index));
    }

    foreach (var error in result.Errors)
    {
        ModelState.AddModelError(string.Empty, error.Description);
    }

    return View(model);
}

    // Modifier un enseignant (GET)
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var teacher = await _userManager.FindByIdAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        var model = new TeacherViewModel
        {
            Firstname = teacher.Firstname,
            Lastname = teacher.Lastname,
            Email = teacher.Email,
            PersonalWebSite = teacher.PersonalWebSite
        };

        return View(model);
    }

    // Modifier un enseignant (POST)
    [HttpPost]
    public async Task<IActionResult> Edit(string id, TeacherViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var teacher = await _userManager.FindByIdAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        teacher.Firstname = model.Firstname;
        teacher.Lastname = model.Lastname;
        teacher.Email = model.Email;
        teacher.PersonalWebSite = model.PersonalWebSite;

        var result = await _userManager.UpdateAsync(teacher);

        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    // Supprimer un enseignant (GET)
    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var teacher = await _userManager.FindByIdAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }
        return View(teacher);
    }

    // Supprimer un enseignant (POST)
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var teacher = await _userManager.FindByIdAsync(id);
        if (teacher != null)
        {
            await _userManager.DeleteAsync(teacher);
        }
        return RedirectToAction(nameof(Index));
    }
}
