namespace TodoApi.Models;

public class TodoItem
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedAt { get; set; } = DateTime.Now;
  public string? Name { get; set; }
  public bool IsComplete { get; set; }
}