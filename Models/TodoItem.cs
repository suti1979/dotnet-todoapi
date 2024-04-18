namespace TodoApi.Models;

public class TodoItem
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string? Name { get; set; }
  public bool IsComplete { get; set; }
}
