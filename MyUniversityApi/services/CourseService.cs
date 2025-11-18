using MyUniversityApi.Models;
using MyUniversityApi.Repositories;

namespace MyUniversityApi.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            // Тут могла б бути бізнес-логіка, наприклад, перевірка, чи не існує вже курс з такою назвою
            return await _courseRepository.AddAsync(course);
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllAsync();
        }
    }
}