using ToDoListApi.Application.Entities;

namespace ToDoListApi.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
