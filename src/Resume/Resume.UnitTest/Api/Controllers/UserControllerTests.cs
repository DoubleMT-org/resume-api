using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Resume.Api.Controllers;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Users;
using Resume.Service.DTOs.Users;
using Resume.Service.Interfaces;
using Xunit;

namespace Resume.UnitTest.Api.Controllers
{
    public class UserControllerTests
    {
        private readonly IUserService _userService;
        private readonly UsersController _userController;
        private readonly IConfiguration _configuration;
        public UserControllerTests()
        {
            _configuration = A.Fake<IConfiguration>();
            _userService = A.Fake<IUserService>();
            _userController = new UsersController(_userService,_configuration);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public async void UserController_GetAsync_ReturnsObject(long id)
        {
            // Arrange
            var user = A.Fake<User>();
            A.CallTo(() => _userService.GetAsync(u => u.Id == id)).Returns(user);

            // Act
            var result = await _userController.GetAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<User>>();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public async void UserController_GetFullyAsync_ReturnsObject(long id)
        {
            // Arrange
            var user = A.Fake<User>();
            A.CallTo(() => _userService.GetAsync(u => u.Id == id)).Returns(user);

            // Act
            var result = await _userController.GetFullyAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<User>>();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void UserController_GetAllAsync_ReturnsObject()
        {
            // Arrange
            var users = A.Fake<IEnumerable<User>>();
            var param = A.Fake<PagenationParams>();

            A.CallTo(() => _userService.GetAllAsync(param, null)).Returns(users);

            // Act
            var result = await _userController.GetAllAsync(param);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void UserController_GetAllFullyAsync_ReturnsObject()
        {
            // Arrange
            var users = A.Fake<IEnumerable<User>>();
            var param = A.Fake<PagenationParams>();

            A.CallTo(() => _userService.GetAllAsync(param, null)).Returns(users);

            // Act
            var result = await _userController.GetAllFullyAsync(param);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void UserController_CreateAsync_ReturnsObject()
        {
            // Arrange
            var userDto = A.Fake<UserForCreationDto>();
            var user = A.Fake<User>();
            A.CallTo(() => _userService.CreateAsync(userDto)).Returns(user);

            // Act
            var result = await _userController.CreateAsync(userDto);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));

        }

        [Theory]
        [InlineData(1)]
        public async void UserController_DeleteAsync_ReturnsObject(long id)
        {
            // Arrange
            A.CallTo(() => _userService.DeleteAsync(user => user.Id == id)).Returns(true);

            // Act
            var result = await _userController.DeleteAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
