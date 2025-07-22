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
        public void CreateUser_ShouldThrowException_WhenEmailIsEmpty()
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
            Assert.That(async () => await sut.CreateUserAsync(request), Throws.ArgumentException.With.Message.EqualTo("Email cannot be null, empty, or whitespace."));
        }

        [Test]
        public void CreateUser_ShouldThrowException_WhenEmailIsNull()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var sut = new CreateUser(userRepository);
            var request = new CreateUserRequest
            {
                Email = null!, // invalid
                Password = "Password123!"
            };
            // Act & Assert
            Assert.That(async () => await sut.CreateUserAsync(request), Throws.ArgumentException.With.Message.EqualTo("Email cannot be null, empty, or whitespace."));
        }

        [Test]
        public void CreateUser_ShouldThrowException_WhenEmailIsWhitespace()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var sut = new CreateUser(userRepository);
            var request = new CreateUserRequest
            {
                Email = " ", // invalid
                Password = "Password123!"
            };
            // Act & Assert
            Assert.That(async () => await sut.CreateUserAsync(request), Throws.ArgumentException.With.Message.EqualTo("Email cannot be null, empty, or whitespace."));
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
                Password = "" // invalid
            };

            // Act & Assert
            Assert.That(async () => await sut.CreateUserAsync(request), Throws.ArgumentException.With.Message.EqualTo("Password cannot be null, empty, or whitespace."));
        }

        [Test]
        public void CreateUser_ShouldThrowException_WhenPasswordIsNull()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var sut = new CreateUser(userRepository);
            var request = new CreateUserRequest
            {
                Email = "test@example.com",
                Password = null! // invalid
            };

            // Act & Assert
            Assert.That(async () => await sut.CreateUserAsync(request), Throws.ArgumentException.With.Message.EqualTo("Password cannot be null, empty, or whitespace."));
        }

        [Test]
        public void CreateUser_ShouldThrowException_WhenPasswordIsWhitespace()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var sut = new CreateUser(userRepository);
            var request = new CreateUserRequest
            {
                Email = "test@example.com",
                Password = " " // invalid
            };

            // Act & Assert
            Assert.That(async () => await sut.CreateUserAsync(request), Throws.ArgumentException.With.Message.EqualTo("Password cannot be null, empty, or whitespace."));
        }
    }
}
