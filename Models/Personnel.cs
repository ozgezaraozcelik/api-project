using System.ComponentModel.DataAnnotations;

namespace PersonnelTrainingAPI.Models
{
    public class Personnel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public bool IsTrainingCompleted { get; set; }

        public DateTime JoinDate { get; set; }
    }
}

