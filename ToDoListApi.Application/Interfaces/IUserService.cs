using ToDoListApi.Application.DTOs;

namespace ToDoListApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<Guid> CreateUserAsync(CreateUserRequest request);
    }
}
