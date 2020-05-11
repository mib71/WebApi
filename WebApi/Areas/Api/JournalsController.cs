using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Entities;
using WebApi.Data;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Areas.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JournalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JournalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Journals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Journal>>> GetJournals()
        {
            return await _context.Journals.ToListAsync();
        }

        // GET: api/Journals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Journal>> GetJournal(int id)
        {
            var journal = await _context.Journals.FindAsync(id);

            if (journal == null)
            {
                return NotFound();
            }

            return journal;
        }

        // PUT: api/Journals/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJournal(int id, Journal journal)
        {
            if (id != journal.Id)
            {
                return BadRequest();
            }

            _context.Entry(journal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JournalExists(id))
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

        // POST: api/Journals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Journal>> PostJournal(Journal journal)
        {
            _context.Journals.Add(journal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJournal", new { id = journal.Id }, journal);
        }

        // DELETE: api/Journals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Journal>> DeleteJournal(int id)
        {
            var journal = await _context.Journals.FindAsync(id);
            if (journal == null)
            {
                return NotFound();
            }

            _context.Journals.Remove(journal);
            await _context.SaveChangesAsync();

            return journal;
        }

        private bool JournalExists(int id)
        {
            return _context.Journals.Any(e => e.Id == id);
        }
    }
}
