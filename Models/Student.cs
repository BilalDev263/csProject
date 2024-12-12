using System.ComponentModel.DataAnnotations;

namespace mvc.Models;

public enum Specialite
{
    L, // Littéraire
    ES, // Économique et Sociale
    S // Scientifique
}

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le prénom est obligatoire.")]
    public string Firstname { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire.")]
    public string Lastname { get; set; }

    [Required(ErrorMessage = "L'âge est obligatoire.")]
    [Range(18, 100, ErrorMessage = "L'âge doit être compris entre 18 et 100 ans.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "La spécialité est obligatoire.")]
    public Specialite Specialite { get; set; }

    [Required(ErrorMessage = "La date d'admission est obligatoire.")]
    [DataType(DataType.Date)]
    public DateTime AdmissionDate { get; set; }
}
