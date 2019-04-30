using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using testasp.Data;

namespace testasp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly TodoContext _context;

        public AccountsController(TodoContext context, ILogger<AccountsController> logger)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public IEnumerable<Account> Getaccounts()
        {
            return _context.accounts;
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _context.accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount([FromRoute] int id, [FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account.id)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.accounts.Add(account);
            await _context.SaveChangesAsync();

            return Ok(account);

            //return CreatedAtAction("GetAccount", new { id = account.id }, account);
        }

        // POST: api/Accounts
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Account sentAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //username password check
            var foundAccount = await Task.Run(() => _context.accounts.SingleOrDefault(x => x.username == sentAccount.username
                && x.password == sentAccount.password));


            if (foundAccount == null)
            {
                return BadRequest(new { message = "Username or password is wrong" });
            }

            return Ok(foundAccount);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _context.accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.accounts.Remove(account);
            await _context.SaveChangesAsync();

            return Ok(account);
        }

        private bool AccountExists(int id)
        {
            return _context.accounts.Any(e => e.id == id);
        }
    }
}

