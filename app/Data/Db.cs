using Microsoft.EntityFrameworkCore;
using TodoApi.app.Models;

namespace TodoApi.app.Data;

public class Db(DbContextOptions<Db> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems { get; init; } = null!;
    public DbSet<Stuff> Stuffs { get; init; }
    public DbSet<Suti> Suties { get; init; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<TodoItem>()
        //     .HasMany(t => t.Stuffs)
        //     .WithOne(s => s.TodoItem)
        //     .HasForeignKey(s => s.TodoItemId);
    }
}

public class CreateTodoItemDto
{
    public required string Name { get; set; }
    public bool IsComplete { get; set; }
}