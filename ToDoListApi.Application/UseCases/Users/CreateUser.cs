using ToDoListApi.Application.DTOs;
using ToDoListApi.Application.Interfaces;

namespace ToDoListApi.Application.UseCases.Users
{
    public class CreateUser(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Guid> CreateUserAsync(CreateUserRequest request)
        {
            Validate(request);
            return await _userRepository.CreateUserAsync(request.Email, request.Password);
        }

        private static void Validate(CreateUserRequest request)
        {
            if (request.Email == string.Empty)
            {
                throw new ArgumentException("Email cannot be empty.");
            }

            if (request.Email is null)
            {
                throw new ArgumentNullException(nameof(request), "Email cannot be null.");
            }

            if (request.Email is " ")
            {
                throw new ArgumentException("Email cannot be whitespace.");
            }

            if (request.Password == string.Empty)
            {
                throw new ArgumentException("Password cannot be empty.");
            }

            if (request.Password is null)
            {
                throw new ArgumentNullException(nameof(request), "Password cannot be null.");
            }

            if (request.Password is " ")
            {
                throw new ArgumentException("Password cannot be whitespace.");
            }
        }
    }
}
