using System.ComponentModel.DataAnnotations;

namespace mvc.Models
{
    public class TeacherViewModel
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Url]
        public string? PersonalWebSite { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères.")]
        public string Password { get; set; }
    }
}
