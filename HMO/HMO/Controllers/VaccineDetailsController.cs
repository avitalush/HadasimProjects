using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Service.CoronaDetailsService;
using Service.VaccineDetailsService;

namespace HMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineDetailsController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IVaccineetailsData _dbStore;
        public VaccineDetailsController(MyDBContext context, IVaccineetailsData dbstore)
        {
            _dbStore = dbstore;
            _context = context;
        }

        // GET: api/VaccineDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VaccineDetail>>> GetVaccineDetails()
        {
          if (_context.VaccineDetails == null)
          {
              return NotFound();
          }
            return await _context.VaccineDetails.ToListAsync();
        }

        // GET: api/VaccineDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VaccineDetail>> GetVaccineDetail(int id)
        {
          if (_context.VaccineDetails == null)
          {
              return NotFound();
          }
            var vaccineDetail = await _context.VaccineDetails.FindAsync(id);

            if (vaccineDetail == null)
            {
                return NotFound();
            }

            return vaccineDetail;
        }

        // PUT: api/VaccineDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaccineDetail(int id, VaccineDetail vaccineDetail)
        {
            if (id != vaccineDetail.VaccinationId)
            {
                return BadRequest();
            }

            _context.Entry(vaccineDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccineDetailExists(id))
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

        // POST: api/VaccineDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VaccineDetail>> PostVaccineDetail(VaccineDetail vaccineDetail)
        {
          if (_context.VaccineDetails == null)
          {
              return Problem("Entity set 'MyDBContext.VaccineDetails'  is null.");
          }
            _context.VaccineDetails.Add(vaccineDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVaccineDetail", new { id = vaccineDetail.VaccinationId }, vaccineDetail);
        }

        // DELETE: api/VaccineDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccineDetail(int id)
        {
            if (_context.VaccineDetails == null)
            {
                return NotFound();
            }
            var vaccineDetail = await _context.VaccineDetails.FindAsync(id);
            if (vaccineDetail == null)
            {
                return NotFound();
            }

            _context.VaccineDetails.Remove(vaccineDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VaccineDetailExists(int id)
        {
            return (_context.VaccineDetails?.Any(e => e.VaccinationId == id)).GetValueOrDefault();
        }
        [HttpPost]
        [Route("/api/addVaccineDetail")]
        public async Task<ActionResult<IEnumerable<VaccineDetail>>> addVaccineDetail(VaccineDetail vaccineDetail)
        {
            vaccineDetail.ReceivingVaccine = vaccineDetail.ReceivingVaccine.Value.ToLocalTime();
            var result = await _dbStore.addVaccination(vaccineDetail);
            if (result == "Added successfully")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
