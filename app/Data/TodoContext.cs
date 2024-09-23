using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
  public DbSet<TodoItem> TodoItems { get; set; } = null!;
}

public class CreateTodoItemDto
{
  public required string Name { get; set; }
  public bool IsComplete { get; set; }
}