using System.ComponentModel.DataAnnotations;

namespace mvc.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "L'adresse e-mail est obligatoire.")]
        [EmailAddress(ErrorMessage = "Veuillez saisir une adresse e-mail valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Se souvenir de moi")]
        public bool RememberMe { get; set; }
    }
}
