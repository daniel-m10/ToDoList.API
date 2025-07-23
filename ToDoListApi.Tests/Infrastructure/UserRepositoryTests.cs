using ToDoListApi.Application.Entities;
using ToDoListApi.Application.Infrastructure;

namespace ToDoListApi.Tests.Infrastructure
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test]
        public async Task CreateUserAsync_ShouldCreateUser_WhenDataIsValid()
        {
            // Arrange
            var repo = new InMemoryUserRepository();
            var user = new User("test@example.com", "hashed-password");

            // Act
            var result = await repo.CreateUserAsync(user);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Email, Is.EqualTo("test@example.com"));
                Assert.That(result.PasswordHash, Is.EqualTo("hashed-password"));
            });
        }

        [Test]
        public async Task CreateUserAsync_ShouldThrowException_WhenEmailExists()
        {
            // Arrange
            var repo = new InMemoryUserRepository();
            var userOne = new User("test@example.com", "hashed-password");

            await repo.CreateUserAsync(userOne);

            var userTwo = new User("test@example.com", "hashed-password");

            // Act & Assert
            Assert.That(async () => await repo.CreateUserAsync(userTwo),
                Throws.ArgumentException.With.Message.EqualTo("User with this email already exists."));
        }


        [Test]
        public async Task GetUserByEmail_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var repo = new InMemoryUserRepository();
            var user = new User("test@example.com", "hashed-password");

            await repo.CreateUserAsync(user);

            // Act
            var result = await repo.GetUserByEmailAsync("test@example.com");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.Email, Is.EqualTo("test@example.com"));
                Assert.That(result.PasswordHash, Is.EqualTo("hashed-password"));
            });
        }

        [Test]
        public async Task GetUserByEmail_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var repo = new InMemoryUserRepository();

            // Act
            var result = await repo.GetUserByEmailAsync("nonexistent@example.com");

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
