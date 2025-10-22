using System.ComponentModel.DataAnnotations;

namespace MyUniversityApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
    }
}
