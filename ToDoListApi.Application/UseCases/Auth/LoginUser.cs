using ToDoListApi.Application.DTOs;
using ToDoListApi.Application.Interfaces;

namespace ToDoListApi.Application.UseCases.Auth
{
    public class LoginUser(IUserRepository userRepository, IPasswordHasher passwordHasher) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<Guid> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentException("Email cannot be null, empty, or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException("Password cannot be null, empty, or whitespace.");
            }

            var user = await _userRepository.GetUserByEmailAsync(request.Email) ?? throw new InvalidOperationException("Invalid credentials.");

            var hashedPassword = _passwordHasher.Hash(request.Password);

            if (user.PasswordHash != hashedPassword)
            {
                throw new InvalidOperationException("Invalid credentials.");
            }

            return user.Id;
        }
    }
}
