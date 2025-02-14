using System;
using System.ComponentModel.DataAnnotations;

namespace mvc.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date de l'événement")]
        [DataType(DataType.DateTime)]
        public DateTime EventDate { get; set; }

        [Required]
        [Range(2, 200)]
        [Display(Name = "Nombre maximum de participants")]
        public int MaxParticipants { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Display(Name = "Date de création")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
