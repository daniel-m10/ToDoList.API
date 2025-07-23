namespace ToDoListApi.Application.DTOs
{
    public class LoginRequest
    {
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
