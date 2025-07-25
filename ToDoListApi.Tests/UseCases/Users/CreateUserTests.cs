﻿using NSubstitute;
using ToDoListApi.Application.DTOs;
using ToDoListApi.Application.Entities;
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
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;

            var sut = new CreateUser(userRepository, passwordHasher);   // System under test

            var request = new CreateUserRequest
            {
                Email = "test@example.com",
                Password = "Secure123!"
            };

            var user = new User(request.Email, passwordHasher.Hash(request.Password));

            userRepository.CreateUserAsync(Arg.Any<User>()).Returns(user);

            // Act
            var result = await sut.CreateUserAsync(request);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result, Is.EqualTo(user.Id));
                Assert.That(user.Email, Is.EqualTo("test@example.com"));
                Assert.That(user.PasswordHash, Is.EqualTo("hashed-Secure123!"));
            });
        }

        [Test]
        public void CreateUser_ShouldThrowException_WhenEmailIsEmpty()
        {
            // Arrange
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;

            var sut = new CreateUser(userRepository, passwordHasher);

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
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;

            var sut = new CreateUser(userRepository, passwordHasher);
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
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;

            var sut = new CreateUser(userRepository, passwordHasher);
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
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;

            var sut = new CreateUser(userRepository, passwordHasher);

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
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;

            var sut = new CreateUser(userRepository, passwordHasher);
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
            var userRepository = UserRepository;
            var passwordHasher = PasswordHasher;

            var sut = new CreateUser(userRepository, passwordHasher);
            var request = new CreateUserRequest
            {
                Email = "test@example.com",
                Password = " " // invalid
            };

            // Act & Assert
            Assert.That(async () => await sut.CreateUserAsync(request), Throws.ArgumentException.With.Message.EqualTo("Password cannot be null, empty, or whitespace."));
        }

        [Test]
        public void CreateUser_ShouldThrowException_WhenUserAlreadyExists()
        {
            // Arrange
            var existingUser = new User("test@example.com", "hashed-password");
            var userRepository = UserRepository;
            userRepository.GetUserByEmailAsync(existingUser.Email).Returns(existingUser);

            var hasher = new FakePasswordHasher();

            var sut = new CreateUser(userRepository, hasher);

            var request = new CreateUserRequest
            {
                Email = "test@example.com",
                Password = "Secure123!"
            };

            // Act & Assert
            Assert.That(async () => await sut.CreateUserAsync(request),
                Throws.InvalidOperationException.With.Message.EqualTo("User with this email already exists."));
        }

        private static IUserRepository UserRepository => Substitute.For<IUserRepository>();

        private static IPasswordHasher PasswordHasher => new FakePasswordHasher();
    }
}
