using MyUniversityApi.Models;

namespace MyUniversityApi.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student> AddAsync(Student student);
        Task<Student> UpdateAsync(Student student);
        Task DeleteAsync(Student student);
    }
}