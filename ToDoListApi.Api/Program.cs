using ToDoListApi.Application.Infrastructure;
using ToDoListApi.Application.Interfaces;
using ToDoListApi.Application.UseCases.Auth;
using ToDoListApi.Application.UseCases.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<IPasswordHasher, Sha256PasswordHasher>();
builder.Services.AddSingleton<IUserService, CreateUser>();
builder.Services.AddSingleton<IAuthService, LoginUser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

