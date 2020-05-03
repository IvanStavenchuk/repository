using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersWebApi.Models;

namespace UsersWepApi.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserItemsController : ControllerBase
    {
        private readonly UserContext _context;

        public UserItemsController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUserItems()
        {
            return await _context.UserItems.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserItem(long id)
        {
            var UserItem = await _context.UserItems.FindAsync(id);

            if (UserItem == null)
            {
                return NotFound();
            }

            return UserItem;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserItem(long id, User UserItem)
        {
            if (id != UserItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(UserItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserItemExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUserItem(User UserItem)
        {
            _context.UserItems.Add(UserItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserItem), new { id = UserItem.Id }, UserItem);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUserItem(long id)
        {
            var UserItem = await _context.UserItems.FindAsync(id);
            if (UserItem == null)
            {
                return NotFound();
            }

            _context.UserItems.Remove(UserItem);
            await _context.SaveChangesAsync();

            return UserItem;
        }

        private bool UserItemExists(long id)
        {
            return _context.UserItems.Any(e => e.Id == id);
        }
    }
}
