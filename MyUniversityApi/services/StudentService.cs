using MyUniversityApi.Models;
using MyUniversityApi.Repositories;

namespace MyUniversityApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            student.EnrollmentDate = DateTime.UtcNow;
            return await _studentRepository.AddAsync(student);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return false;
            }
            await _studentRepository.DeleteAsync(student);
            return true;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateStudentAsync(int id, Student student)
        {
            if (id != student.Id)
            {
                return false; 
            }
            await _studentRepository.UpdateAsync(student);
            return true;
        }
    }
}
