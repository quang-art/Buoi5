using System.ComponentModel.DataAnnotations;

namespace Buoi5.Models
{
    public class Students
    {
        [Key]
        public int studentId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please select a grade")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid grade")]
        public int gradeId { get; set; }

        public Grades? Grade { get; set; } // Make nullable to avoid potential issues
    }
}