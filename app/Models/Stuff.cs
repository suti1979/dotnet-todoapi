using TodoApi.Models;
public class Stuff
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public required string Name { get; set; }
  public Guid TodoItemId { get; set; }
  public TodoItem TodoItem { get; set; } = null!;
}