using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserAccountService.Business;
using UserAccountService.Controllers;
using UserAccountService.DataAccess.UserData;
using UserAccountService.Models;
using Xunit;

namespace UserAccountService.Tests
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IValidationService> _mockValidationService;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockValidationService = new Mock<IValidationService>();
            _controller = new UsersController(_mockUserRepository.Object, _mockValidationService.Object);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnOkResult_WhenUserIsValid()
        {
            
            var user = new User { FirstName = "John", LastName = "Doe", Email = "john@example.com" };
            var deviceType = "mail"; 
            _mockUserRepository.Setup(repo => repo.AddUserAsync(user)).Returns(Task.CompletedTask);
            _mockValidationService.Setup(v => v.ValidateUser(user, deviceType)).Verifiable();

            
            var context = new DefaultHttpContext();
            context.Request.Headers["x-Device"] = deviceType;
            _controller.ControllerContext.HttpContext = context;

            
            var result = await _controller.CreateUser(user, deviceType);

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user, okResult.Value);
            _mockValidationService.Verify(v => v.ValidateUser(user, deviceType), Times.Once);
            _mockUserRepository.Verify(repo => repo.AddUserAsync(user), Times.Once);
        }
    }
}
