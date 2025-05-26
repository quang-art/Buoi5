using System.ComponentModel.DataAnnotations;

namespace Buoi5.Models
{
    public class Grades
    {
        [Key]
        public int gradeId { get; set; }
        public string gradeName { get; set; }
        public List<Students>Students{ get; set; }
    }
}
