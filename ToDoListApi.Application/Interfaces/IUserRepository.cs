namespace ToDoListApi.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(string email, string password);
    }
}
