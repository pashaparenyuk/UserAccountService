using Microsoft.AspNetCore.Mvc;
using UserAccountService.Business;
using UserAccountService.DataAccess.UserData;
using UserAccountService.Models;

namespace UserAccountService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidationService _validationService;

        public UsersController(IUserRepository userRepository, IValidationService validationService)
        {
            _userRepository = userRepository;
            _validationService = validationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user, [FromHeader(Name = "x-Device")] string deviceType)
        {            
            _validationService.ValidateUser(user, deviceType);

            await _userRepository.AddUserAsync(user);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers(string? lastName, string? firstName, string? middleName, string? phoneNumber, string? email)
        {
            var users = await _userRepository.SearchUsersAsync(lastName, firstName, middleName, phoneNumber, email);
            return Ok(users);
        }
    }
}
