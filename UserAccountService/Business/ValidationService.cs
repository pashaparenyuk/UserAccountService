using UserAccountService.Models;

namespace UserAccountService.Business
{
    public class ValidationService : IValidationService
    {
        public void ValidateUser(User user, string deviceType)
        {
            switch (deviceType)
            {
                case "mail":
                    if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.Email))
                        throw new ArgumentException("Name and email are required.");
                    break;
                case "mobile":
                    if (string.IsNullOrEmpty(user.PhoneNumber))
                        throw new ArgumentException("Phone number is required.");
                    break;
                case "web":
                    if (string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.PhoneNumber) ||
                        string.IsNullOrEmpty(user.PassportNumber) || string.IsNullOrEmpty(user.PlaceOfBirth) || user.DateOfBirth == default)
                        throw new ArgumentException("All fields except email and residential address are required.");
                    break;
                default:
                    throw new ArgumentException("Unknown device type.");
            }
        }
    }
}
