﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatient(int id)
        {
            var patient = _context.Patients
                .Include(p => p.Journals)
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient updatePatient)
        {
            var patient = _context.Patients
                .FirstOrDefault(p => p.Id == id);

            if (patient == null) return NotFound();


            patient.FirstName = updatePatient.FirstName;
            patient.LastName = updatePatient.LastName;
            patient.SocialSecurityNumber = updatePatient.SocialSecurityNumber;

            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            var newPatient = patient;

            if (newPatient == null)
            {
                return BadRequest();
            }
            
            
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = newPatient.Id }, newPatient);
        }

        // POST: api/Patients/5/journal
        [HttpPost("{id}/journal")]
        public async Task<ActionResult<Patient>> PostPatientJornal(int id, Journal journal)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            var addJournal = new Journal();
                                    
            addJournal.EntryBy = journal.EntryBy;
            addJournal.Date = journal.Date;
            addJournal.Comment = journal.Comment;

            patient.Journals.Add(addJournal);
            await _context.SaveChangesAsync();

            return Created($"/api/journals/{addJournal.Id}", addJournal);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public ActionResult<Patient> DeletePatient(int id)
        {
            var patient = _context.Patients.Include(j => j.Journals).FirstOrDefault(p => p.Id == id);
            
            if (patient == null)
            {
                return NotFound();
            }
                        
            _context.Patients.Remove(patient);
            _context.SaveChanges();

            return Ok(patient);
        }
    }
}
