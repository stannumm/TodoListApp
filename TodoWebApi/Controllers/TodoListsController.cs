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
    [Route("api/{accountid}/lists")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoListsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Accounts/{accountid}/lists
        [HttpGet]
        public IEnumerable<TodoList> Index([FromRoute]int accountid)
        {
            return _context.TodoList.Where(account => account.AccountId == accountid);
        }

        // GET: api/TodoLists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoList([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoList = await _context.TodoList.FindAsync(id);

            if (todoList == null)
            {
                return NotFound();
            }

            return Ok(todoList);
        }

        // POST: api/TodoLists
        [HttpPost]
        public async Task<IActionResult> PostTodoList([FromBody] TodoList todoList, [FromRoute]int accountid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            todoList.AccountId = accountid;
           _context.TodoList.Add(todoList);

            await _context.SaveChangesAsync();

            return Ok(todoList);
          //  return CreatedAtAction("GetTodoList", new { id = todoList.id }, todoList);
        }

        // DELETE: api/TodoLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoList = await _context.TodoList.FindAsync(id);
            if (todoList == null)
            {
                return NotFound();
            }

            _context.TodoList.Remove(todoList);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TodoListExists(int id)
        {
            return _context.TodoList.Any(e => e.id == id);
        }
    }
}