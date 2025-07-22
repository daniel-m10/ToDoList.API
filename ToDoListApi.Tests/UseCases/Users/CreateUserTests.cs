using NSubstitute;
using ToDoListApi.Application.DTOs;
using ToDoListApi.Application.Interfaces;
using ToDoListApi.Application.UseCases.Users;

namespace ToDoListApi.Tests.UseCases.Users
{
    [TestFixture]
    public class CreateUserTests
    {
        [Test]
        public async Task CreateUser_ShouldReturnUserId_WhenDataIsValid()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var sut = new CreateUser(userRepository);   // System under test

            var request = new CreateUserRequest
            {
                Email = "test@example.com",
                Password = "Secure123!"
            };

            var expectedUserId = Guid.NewGuid();

            userRepository
                .CreateUserAsync(request.Email, request.Password)
                .Returns(expectedUserId);

            // Act
            var result = await sut.CreateUserAsync(request);

            // Assert
            Assert.That(result, Is.EqualTo(expectedUserId));
        }

        [Test]
        public void Create_User_ShouldThrowException_WhenEmailIsEmpty()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var sut = new CreateUser(userRepository);

            var request = new CreateUserRequest
            {
                Email = string.Empty,
                Password = "Password123!"
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await sut.CreateUserAsync(request);
            }, "Email cannot be empty.");
        }

        [Test]
        public void CreateUser_ShouldThrowException_WhenPasswordIsEmpty()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var sut = new CreateUser(userRepository);

            var request = new CreateUserRequest
            {
                Email = "test@example.com",
                Password = "" // inválido
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await sut.CreateUserAsync(request);
            }, "Password cannot be empty.");
        }

    }
}
