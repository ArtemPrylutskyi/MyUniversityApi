using MyUniversityApi.Models;

namespace MyUniversityApi.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> AddAsync(Course course);
    }
}