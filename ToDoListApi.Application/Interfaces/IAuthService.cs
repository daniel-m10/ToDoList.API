using ToDoListApi.Application.DTOs;

namespace ToDoListApi.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Guid> LoginAsync(LoginRequest request);
    }
}
