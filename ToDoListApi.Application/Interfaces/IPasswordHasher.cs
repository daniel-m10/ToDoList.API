﻿namespace ToDoListApi.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
    }
}
