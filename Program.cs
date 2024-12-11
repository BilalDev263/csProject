using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvc.Data;
using mvc.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services au conteneur.
builder.Services.AddControllersWithViews();

// Configuration de la version MySQL.
var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion)
);

// Configuration d'Identity avec les rôles
builder.Services.AddIdentity<Teacher, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();

var app = builder.Build();

// Méthode pour configurer les rôles et l'utilisateur initial
async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<Teacher>>();

    // Créer les rôles Teacher et Student
    if (!await roleManager.RoleExistsAsync("Teacher"))
    {
        await roleManager.CreateAsync(new IdentityRole("Teacher"));
    }
    if (!await roleManager.RoleExistsAsync("Student"))
    {
        await roleManager.CreateAsync(new IdentityRole("Student"));
    }

    // Créer un utilisateur Teacher initial
    var teacherUser = await userManager.FindByNameAsync("AdminTeacher");
    if (teacherUser == null)
    {
        var teacher = new Teacher
        {
            UserName = "AdminTeacher",
            Email = "teacher@example.com",
            Firstname = "Admin",
            Lastname = "Teacher",
            PersonalWebSite = "https://defaultwebsite.com", // Ajouter une valeur par défaut
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(teacher, "Teacher@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(teacher, "Teacher");
        }
    }
}


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateRoles(services);
}

// Configuration du pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
