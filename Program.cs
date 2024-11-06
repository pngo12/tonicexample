using Microsoft.EntityFrameworkCore;
using TodoApi.DatabaseContext;
using TodoApi.Repository;
using TodoApi.Services;
using TodoApi.TodoService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<TodoContext>
    (x => x.UseInMemoryDatabase("TodoApp"));

// Register DI items
builder.Services
    .AddScoped<ITodoService, TodoService>()
    .AddScoped<ITodoRepository, TodoRepository>()
    ;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
