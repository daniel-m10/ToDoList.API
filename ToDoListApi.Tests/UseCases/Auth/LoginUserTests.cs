using NSubstitute;
using ToDoListApi.Application.DTOs;
using ToDoListApi.Application.Entities;
using ToDoListApi.Application.Interfaces;
using ToDoListApi.Application.UseCases.Auth;

namespace ToDoListApi.Tests.UseCases.Auth
{
    [TestFixture]
    public class LoginUserTests
    {
        [Test]
        public async Task LoginUser_ShouldReturnUserId_WhenCredentialsAreValid()
        {
            // Arrange
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;
            var sut = new LoginUser(userRepository, passwordHasher);

            var request = new LoginRequest
            {
                Email = "test@example.com",
                Password = "MySecurePassword"
            };

            var hashedPassword = "hashed-password";
            var user = new User(request.Email, hashedPassword);

            passwordHasher.Hash(request.Password).Returns(hashedPassword);
            userRepository.GetUserByEmailAsync(request.Email).Returns(user);

            // Act
            var result = await sut.LoginAsync(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(user.Id));
                Assert.That(user.Email, Is.EqualTo("test@example.com"));
                Assert.That(user.PasswordHash, Is.EqualTo(hashedPassword));
            });
        }

        [Test]
        public void LoginUser_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;
            var sut = new LoginUser(userRepository, passwordHasher);

            var request = new LoginRequest
            {
                Email = "notfound@example.com",
                Password = "irrelevant"
            };

            userRepository.GetUserByEmailAsync(request.Email).Returns((User?)null);

            // Act & Assert
            Assert.That(async () => await sut.LoginAsync(request),
                Throws.InvalidOperationException.With.Message.EqualTo("Invalid credentials."));
        }

        [Test]
        public void LoginUser_ShouldThrowException_WHenPasswordDoesNotMatch()
        {
            // Arrange
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;
            var sut = new LoginUser(userRepository, passwordHasher);

            var request = new LoginRequest
            {
                Email = "test@example.com",
                Password = "wrong-password"
            };

            var storedPasswordHash = "hashed-correct-password";
            var user = new User(request.Email, storedPasswordHash);

            userRepository.GetUserByEmailAsync(request.Email).Returns(user);
            passwordHasher.Hash(request.Password).Returns("hashed-wrong-password");

            // Act & Assert
            Assert.That(async () => await sut.LoginAsync(request),
                Throws.InvalidOperationException.With.Message.EqualTo("Invalid credentials."));
        }

        [Test]
        public void LoginUser_ShouldThrowException_WhenEmailIsEmpty()
        {
            // Arrange
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;
            var sut = new LoginUser(userRepository, passwordHasher);

            var request = new LoginRequest
            {
                Email = string.Empty,
                Password = "SomePassword"
            };

            // Act & Assert
            Assert.That(async () => await sut.LoginAsync(request),
                Throws.ArgumentException.With.Message.EqualTo("Email cannot be null, empty, or whitespace."));
        }

        [Test]
        public void LoginUser_ShouldThrowException_WhenEmailIsNull()
        {
            // Arrange
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;
            var sut = new LoginUser(userRepository, passwordHasher);
            var request = new LoginRequest
            {
                Email = null!,
                Password = "SomePassword"
            };
            // Act & Assert
            Assert.That(async () => await sut.LoginAsync(request),
                Throws.ArgumentException.With.Message.EqualTo("Email cannot be null, empty, or whitespace."));
        }

        [Test]
        public void LoginUser_ShouldThrowException_WhenEmailIsWhitespace()
        {
            // Arrange
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;
            var sut = new LoginUser(userRepository, passwordHasher);
            var request = new LoginRequest
            {
                Email = "   ",
                Password = "SomePassword"
            };
            // Act & Assert
            Assert.That(async () => await sut.LoginAsync(request),
                Throws.ArgumentException.With.Message.EqualTo("Email cannot be null, empty, or whitespace."));
        }

        [Test]
        public void LoginUser_ShouldThrowException_WhenPasswordIsEmpty()
        {
            // Arrange
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;
            var sut = new LoginUser(userRepository, passwordHasher);

            var request = new LoginRequest
            {
                Email = "test@example.com",
                Password = string.Empty
            };

            // Act & Assert
            Assert.That(async () => await sut.LoginAsync(request),
                Throws.ArgumentException.With.Message.EqualTo("Password cannot be null, empty, or whitespace."));
        }

        [Test]
        public void LoginUser_ShouldThrowException_WhenPasswordIsNull()
        {
            // Arrange
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;
            var sut = new LoginUser(userRepository, passwordHasher);

            var request = new LoginRequest
            {
                Email = "test@example.com",
                Password = null!
            };

            // Act & Assert
            Assert.That(async () => await sut.LoginAsync(request),
                Throws.ArgumentException.With.Message.EqualTo("Password cannot be null, empty, or whitespace."));
        }

        [Test]
        public void LoginUser_ShouldThrowException_WhenPasswordIsWhitespace()
        {
            // Arrange
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;
            var sut = new LoginUser(userRepository, passwordHasher);

            var request = new LoginRequest
            {
                Email = "test@example.com",
                Password = "     "
            };

            // Act & Assert
            Assert.That(async () => await sut.LoginAsync(request),
                Throws.ArgumentException.With.Message.EqualTo("Password cannot be null, empty, or whitespace."));
        }

        private static IUserRepository UserRepository => Substitute.For<IUserRepository>();
        private static IPasswordHasher PasswordHasher => Substitute.For<IPasswordHasher>();
    }
}
