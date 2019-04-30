using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testasp.Data;

namespace testasp.Controllers
{
    [Route("api/{listid}/items")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public IEnumerable<TodoItem> Index([FromRoute]int listid)
        {
            return _context.TodoItem.Where(list => list.TodoListId == listid);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoItem = await _context.TodoItem.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<IActionResult> PostTodoItem([FromBody] TodoItem todoItem, [FromRoute]int listid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            todoItem.TodoListId = listid;
            _context.TodoItem.Add(todoItem);
            await _context.SaveChangesAsync();
            return Ok(todoItem);
            //return CreatedAtAction("GetTodoItem", new { id = todoItem.id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItem.Remove(todoItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItem.Any(e => e.id == id);
        }
    }
}