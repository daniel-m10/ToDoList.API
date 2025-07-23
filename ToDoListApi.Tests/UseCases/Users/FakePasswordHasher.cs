using ToDoListApi.Application.Interfaces;

namespace ToDoListApi.Tests.UseCases.Users
{
    public class FakePasswordHasher : IPasswordHasher
    {
        public string Hash(string password) => $"hashed-{password}";
    }
}
