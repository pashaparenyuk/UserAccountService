using UserAccountService.Models;

namespace UserAccountService.Business
{
    public interface IValidationService
    {
        void ValidateUser(User user, string deviceType);
    }
}
