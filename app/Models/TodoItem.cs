using System.ComponentModel.DataAnnotations;

namespace TodoApi.app.Models;

public class TodoItem
{
  public Guid Id { get; init; } = Guid.NewGuid();
  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
  [StringLength(50)]
  public required string Name { get; init; }
  public bool IsComplete { get; init; }
  
  public ICollection<Suti> Suties { get; init; }
  
}
