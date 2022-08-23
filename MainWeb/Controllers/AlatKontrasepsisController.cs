using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MainWeb.Data;
using MainWeb.Models;

namespace MainWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlatKontrasepsisController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlatKontrasepsisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AlatKontrasepsis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlatKontrasepsi>>> GetAlatKontrasepsi()
        {
          if (_context.AlatKontrasepsi == null)
          {
              return NotFound();
          }
            return await _context.AlatKontrasepsi.ToListAsync();
        }

        // GET: api/AlatKontrasepsis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlatKontrasepsi>> GetAlatKontrasepsi(int id)
        {
          if (_context.AlatKontrasepsi == null)
          {
              return NotFound();
          }
            var alatKontrasepsi = await _context.AlatKontrasepsi.FindAsync(id);

            if (alatKontrasepsi == null)
            {
                return NotFound();
            }

            return alatKontrasepsi;
        }

        // PUT: api/AlatKontrasepsis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlatKontrasepsi(int id, AlatKontrasepsi alatKontrasepsi)
        {
            if (id != alatKontrasepsi.Id)
            {
                return BadRequest();
            }

            _context.Entry(alatKontrasepsi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlatKontrasepsiExists(id))
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

        // POST: api/AlatKontrasepsis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlatKontrasepsi>> PostAlatKontrasepsi(AlatKontrasepsi alatKontrasepsi)
        {
          if (_context.AlatKontrasepsi == null)
          {
              return Problem("Entity set 'ApplicationDbContext.AlatKontrasepsi'  is null.");
          }
            _context.AlatKontrasepsi.Add(alatKontrasepsi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlatKontrasepsi", new { id = alatKontrasepsi.Id }, alatKontrasepsi);
        }

        // DELETE: api/AlatKontrasepsis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlatKontrasepsi(int id)
        {
            if (_context.AlatKontrasepsi == null)
            {
                return NotFound();
            }
            var alatKontrasepsi = await _context.AlatKontrasepsi.FindAsync(id);
            if (alatKontrasepsi == null)
            {
                return NotFound();
            }

            _context.AlatKontrasepsi.Remove(alatKontrasepsi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlatKontrasepsiExists(int id)
        {
            return (_context.AlatKontrasepsi?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
