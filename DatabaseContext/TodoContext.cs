using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.DatabaseContext;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options) { }

    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TodoApp");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>()
            .HasIndex(x => x.TodoId);

        base.OnModelCreating(modelBuilder);
    }
}
