using MyUniversityApi.Models;

namespace MyUniversityApi.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course> CreateCourseAsync(Course course);
    }
}