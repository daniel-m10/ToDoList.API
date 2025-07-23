using System.Collections.Concurrent;
using ToDoListApi.Application.Entities;
using ToDoListApi.Application.Interfaces;

namespace ToDoListApi.Application.Infrastructure
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<string, User> _users = new();

        public Task<User> CreateUserAsync(User user)
        {
            if (_users.ContainsKey(user.Email))
            {
                throw new ArgumentException("User with this email already exists.");
            }

            _users[user.Email] = user;

            return Task.FromResult(user);
        }
    }
}
