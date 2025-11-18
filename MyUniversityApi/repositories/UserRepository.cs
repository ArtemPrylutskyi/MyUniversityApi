using Microsoft.EntityFrameworkCore;
using MyUniversityApi.Data;
using MyUniversityApi.Models;

namespace MyUniversityApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UniversityDbContext _context;

        public UserRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}