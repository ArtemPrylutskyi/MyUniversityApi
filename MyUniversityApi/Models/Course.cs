using System.ComponentModel.DataAnnotations;

namespace MyUniversityApi.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
    }
}
