using UserAccountService.Models;

namespace UserAccountService.DataAccess.UserData
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>> SearchUsersAsync(string lastName, string firstName, string middleName, string phoneNumber, string email);
        Task AddUserAsync(User user);
    }
}
