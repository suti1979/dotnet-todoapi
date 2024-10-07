using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
  public DbSet<TodoItem> TodoItems { get; set; } = null!;
  public DbSet<Stuff> Stuffs { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<TodoItem>()
      .HasMany(t => t.Stuffs)
      .WithOne(s => s.TodoItem)
      .HasForeignKey(s => s.TodoItemId);
  }

}

public class CreateTodoItemDto
{
  public required string Name { get; set; }
  public bool IsComplete { get; set; }
}