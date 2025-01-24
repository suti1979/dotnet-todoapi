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
    public class TodoItemsController(Db db, IChatService chatService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await db
                .TodoItems
                .Include(t => t.Suties)
                .ToListAsync();
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(Guid id)
        {
            var todoItem = await db.TodoItems
            .Include(t => t.Suties).FirstOrDefaultAsync(t => t.Id == id);

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

            db.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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
                UpdatedAt = DateTime.UtcNow,
                Suties =
                [
                    new Suti
                    {
                        Id = Guid.NewGuid(),
                        Name = createTodoItemDto.Name,

                    }
                ]
            };

            db.TodoItems.Add(todoItem);
            await db.SaveChangesAsync();
            await chatService.Refresh("COURIER-" + Guid.NewGuid());
  

            // Fix: Add route values containing the id
            return CreatedAtAction(
                nameof(GetTodoItem), 
                new { id = todoItem.Id }, // Add route values
                todoItem                  // Response body
            );
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            var todoItem = await db.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            db.TodoItems.Remove(todoItem);
            await db.SaveChangesAsync();
            await chatService.Refresh("COURIER-" + Guid.NewGuid());

            return NoContent();
        }
        
        [HttpGet("suties")]
        public async Task<ActionResult<IEnumerable<Suti>>> GetSutiItems()
        {
           // get items has Suti
            return await db.Suties.ToListAsync();
        }
        
        [HttpGet("stuff")]
        public Guid GetStuffs()
        {
            return Guid.NewGuid();
        }
        
        [HttpPost("stuff/{id:guid}")]
        public async Task<Guid> GetStuffs(Guid id)
        {
            await chatService.Refresh("STUFF-" + Guid.NewGuid());
            return id;
        }
        

        private bool TodoItemExists(Guid id)
        {
            return db.TodoItems.Any(e => e.Id == id);
        }
    }
}
