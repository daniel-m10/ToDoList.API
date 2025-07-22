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
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentException("Email cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException("Password cannot be empty.");
            }
        }
    }
}
