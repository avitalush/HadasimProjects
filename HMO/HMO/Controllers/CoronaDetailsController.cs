using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Service.CoronaDetailsService;
using Service.UsersService;

namespace HMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoronaDetailsController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly ICoronaDetailsData _dbStore;
        public CoronaDetailsController(MyDBContext context, ICoronaDetailsData dbstore)
        {
            _dbStore = dbstore;
            _context = context;
        }

        // GET: api/CoronaDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoronaDetail>>> GetCoronaDetails()
        {
          if (_context.CoronaDetails == null)
          {
              return NotFound();
          }
            return await _context.CoronaDetails.ToListAsync();
        }

        // GET: api/CoronaDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoronaDetail>> GetCoronaDetail(int id)
        {
          if (_context.CoronaDetails == null)
          {
              return NotFound();
          }
            var coronaDetail = await _context.CoronaDetails.FindAsync(id);

            if (coronaDetail == null)
            {
                return NotFound();
            }

            return coronaDetail;
        }

        // PUT: api/CoronaDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoronaDetail(int id, CoronaDetail coronaDetail)
        {
            if (id != coronaDetail.Code)
            {
                return BadRequest();
            }

            _context.Entry(coronaDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoronaDetailExists(id))
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

        // POST: api/CoronaDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CoronaDetail>> PostCoronaDetail(CoronaDetail coronaDetail)
        {
          if (_context.CoronaDetails == null)
          {
              return Problem("Entity set 'MyDBContext.CoronaDetails'  is null.");
          }
            _context.CoronaDetails.Add(coronaDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoronaDetail", new { id = coronaDetail.Code }, coronaDetail);
        }

        // DELETE: api/CoronaDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoronaDetail(int id)
        {
            if (_context.CoronaDetails == null)
            {
                return NotFound();
            }
            var coronaDetail = await _context.CoronaDetails.FindAsync(id);
            if (coronaDetail == null)
            {
                return NotFound();
            }

            _context.CoronaDetails.Remove(coronaDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoronaDetailExists(int id)
        {
            return (_context.CoronaDetails?.Any(e => e.Code == id)).GetValueOrDefault();
        }
        [HttpPost]
        [Route("/api/addCoronaDetail")]
        public async Task<ActionResult<IEnumerable<CoronaDetail>>> addCoronaDetail(CoronaDetail coronaDetail)
        {
            coronaDetail.RecoveryDate = coronaDetail.RecoveryDate.Value.ToLocalTime();
            coronaDetail.PositiveAnswerDate = coronaDetail.PositiveAnswerDate.Value.ToLocalTime();


            var result = await _dbStore.addCoronaDetail(coronaDetail);
            if (result == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        [Route("/api/graphsData")]

        public async Task<string> GetActivePatientsLastMonth()
        {
            var result = await _dbStore.listOfCovid();


            return result;
        }
    }
}
