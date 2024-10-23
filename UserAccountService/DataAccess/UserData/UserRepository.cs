using Microsoft.EntityFrameworkCore;
using UserAccountService.Models;

namespace UserAccountService.DataAccess.UserData
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string lastName, string firstName, string middleName, string phoneNumber, string email)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(u => u.LastName.Contains(lastName));
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(u => u.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(middleName))
            {
                query = query.Where(u => u.MiddleName.Contains(middleName));
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                query = query.Where(u => u.PhoneNumber.Contains(phoneNumber));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(u => u.Email.Contains(email));
            }

            return await query.ToListAsync();
        }


        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
