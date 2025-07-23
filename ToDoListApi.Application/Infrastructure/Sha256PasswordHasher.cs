using System.Security.Cryptography;
using System.Text;
using ToDoListApi.Application.Interfaces;

namespace ToDoListApi.Application.Infrastructure
{
    public class Sha256PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA256.HashData(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
