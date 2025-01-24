using System.ComponentModel.DataAnnotations;

namespace TodoApi.app.Models;

public class Suti
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [StringLength(42)]
    public required string Name { get; set; }
    
    public TodoItem TodoItem { get; set; }
}