using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DepartmentWepApi.Models;
using Common.Objects;

namespace DepartmentWepApi.Controllers
{
    [Route("api/Departments")]
    [ApiController]
    public class DepartmentItemsController : ControllerBase
    {
        private readonly DepartmentContext _context;

        public DepartmentItemsController(DepartmentContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartmentItems()
        {
            return await _context.DepartmentItems.ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentItem(long id)
        {
            var DepartmentItem = await _context.DepartmentItems.FindAsync(id);

            if (DepartmentItem == null)
            {
                return NotFound();
            }

            return DepartmentItem;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartmentItem(long id, Department DepartmentItem)
        {
            if (id != DepartmentItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(DepartmentItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentItemExists(id))
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

        // POST: api/Departments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartmentItem(Department DepartmentItem)
        {
            _context.DepartmentItems.Add(DepartmentItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartmentItem), new { id = DepartmentItem.Id }, DepartmentItem);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartmentItem(long id)
        {
            var DepartmentItem = await _context.DepartmentItems.FindAsync(id);
            if (DepartmentItem == null)
            {
                return NotFound();
            }

            _context.DepartmentItems.Remove(DepartmentItem);
            await _context.SaveChangesAsync();

            return DepartmentItem;
        }

        private bool DepartmentItemExists(long id)
        {
            return _context.DepartmentItems.Any(e => e.Id == id);
        }
    }
}
