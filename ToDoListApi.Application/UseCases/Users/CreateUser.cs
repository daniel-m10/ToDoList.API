using ToDoListApi.Application.DTOs;
using ToDoListApi.Application.Entities;
using ToDoListApi.Application.Interfaces;

namespace ToDoListApi.Application.UseCases.Users
{
    public class CreateUser(IUserRepository userRepository, IPasswordHasher hasher) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _hasher = hasher;

        public async Task<Guid> CreateUserAsync(CreateUserRequest request)
        {
            Validate(request);

            var passwordHash = _hasher.Hash(request.Password);
            var user = new User(request.Email, passwordHash);

            var createdUser = await _userRepository.CreateUserAsync(user);

            return createdUser.Id;
        }

        private static void Validate(CreateUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentException("Email cannot be null, empty, or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException("Password cannot be null, empty, or whitespace.");
            }
        }
    }
}
