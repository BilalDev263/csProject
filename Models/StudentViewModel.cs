using System.ComponentModel.DataAnnotations;

namespace mvc.Models
{
    public class StudentViewModel
    {
        [Required(ErrorMessage = "Le prénom est requis.")]
        [StringLength(50, ErrorMessage = "Le prénom doit comporter au maximum 50 caractères.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(50, ErrorMessage = "Le nom doit comporter au maximum 50 caractères.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "L'âge est requis.")]
        [Range(1, 120, ErrorMessage = "L'âge doit être compris entre 1 et 120.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "La spécialité est requise.")]
        [EnumDataType(typeof(Specialite), ErrorMessage = "La spécialité choisie est invalide.")]
        public Specialite Specialite { get; set; } // Utilise l'énumération existante

        [Required(ErrorMessage = "La date d'admission est requise.")]
        [DataType(DataType.Date, ErrorMessage = "La date d'admission doit être une date valide.")]
        public DateTime AdmissionDate { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "Veuillez entrer une adresse email valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Le mot de passe doit comporter au moins 8 caractères.")]
        public string Password { get; set; }
    }
}
