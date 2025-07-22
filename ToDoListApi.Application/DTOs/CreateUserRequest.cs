namespace ToDoListApi.Application.DTOs;

public class CreateUserRequest
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}

