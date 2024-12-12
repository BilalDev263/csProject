using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using mvc.Models;
using System.Linq;
using System.Threading.Tasks;

public class StudentController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Teacher> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public StudentController(
        ApplicationDbContext context,
        UserManager<Teacher> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // Liste des étudiants
    public IActionResult Index()
    {
        var students = _context.Students.ToList();
        return View(students);
    }

    // Détails d'un étudiant
    public IActionResult Details(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // Ajouter un étudiant (GET)
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    // Ajouter un étudiant (POST)
    [HttpPost]
    public async Task<IActionResult> Add(StudentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Créer un utilisateur Identity pour l'étudiant
        var user = new Teacher
        {
            UserName = model.Firstname + model.Lastname,
            Email = model.Email,
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            PersonalWebSite = "https://defaultstudentwebsite.com"
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Ajouter le rôle "Student"
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Student"));
            }

            await _userManager.AddToRoleAsync(user, "Student");

            // Ajouter l'entité Student dans la base de données
            var student = new Student
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Age = model.Age,
                Specialite = model.Specialite,
                AdmissionDate = model.AdmissionDate
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Ajouter les erreurs au modèle si la création échoue
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    // Modifier un étudiant (GET)
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null)
        {
            return NotFound();
        }

        var model = new StudentViewModel
        {
            Firstname = student.Firstname,
            Lastname = student.Lastname,
            Age = student.Age,
            Specialite = student.Specialite,
            AdmissionDate = student.AdmissionDate,
            Email = ""
        };

        return View(model);
    }

    // Modifier un étudiant (POST)
    [HttpPost]
    public IActionResult Edit(int id, StudentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var student = _context.Students.Find(id);
        if (student == null)
        {
            return NotFound();
        }

        // Mise à jour des champs
        student.Firstname = model.Firstname;
        student.Lastname = model.Lastname;
        student.Age = model.Age;
        student.Specialite = model.Specialite;
        student.AdmissionDate = model.AdmissionDate;

        _context.Students.Update(student);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // Supprimer un étudiant (GET)
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // Supprimer un étudiant (POST)
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var student = _context.Students.Find(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}
