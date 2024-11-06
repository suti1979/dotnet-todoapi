using Dumpify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.app.Data;
using TodoApi.app.Interfaces;
using TodoApi.app.Models;

namespace TodoApi.app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController(TodoContext context, IChatService chatService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await context
                .TodoItems
                .Include(t => t.Stuffs)
                .ToListAsync();
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(Guid id)
        {
            var todoItem = await context.TodoItems
            .Include(t => t.Stuffs).FirstOrDefaultAsync(t => t.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
        
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
        {
            todoItem.Dump();
            
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
                await chatService.Refresh("COURIER-" + Guid.NewGuid());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // return  CreatedAtAction(nameof(GetTodoItem), todoItem);
            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(CreateTodoItemDto createTodoItemDto)
        {
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Name = createTodoItemDto.Name,
                IsComplete = createTodoItemDto.IsComplete,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
                
            };

            context.TodoItems.Add(todoItem);
            await context.SaveChangesAsync();
            await chatService.Refresh("COURIER-" + Guid.NewGuid());
  

            return CreatedAtAction(nameof(GetTodoItem), todoItem);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            var todoItem = await context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            context.TodoItems.Remove(todoItem);
            await context.SaveChangesAsync();
            await chatService.Refresh("COURIER-" + Guid.NewGuid());

            return NoContent();
        }
        
        [HttpGet("/stuff")]
        public Guid GetStuffs()
        {
            return Guid.NewGuid();
        }
        

        private bool TodoItemExists(Guid id)
        {
            return context.TodoItems.Any(e => e.Id == id);
        }
    }
}
