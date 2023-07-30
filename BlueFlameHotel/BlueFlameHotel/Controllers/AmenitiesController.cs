using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlueFlameHotel.Data;
using BlueFlameHotel.Models;

namespace BlueFlameHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly BlueFlameHotelContext _context;

        public AmenitiesController(BlueFlameHotelContext context)
        {
            _context = context;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amenities>>> GetAmenities_1()
        {
          if (_context.Amenities_1 == null)
          {
              return NotFound();
          }
            return await _context.Amenities_1.ToListAsync();
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amenities>> GetAmenities(int id)
        {
          if (_context.Amenities_1 == null)
          {
              return NotFound();
          }
            var amenities = await _context.Amenities_1.FindAsync(id);

            if (amenities == null)
            {
                return NotFound();
            }

            return amenities;
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenities(int id, Amenities amenities)
        {
            if (id != amenities.ID)
            {
                return BadRequest();
            }
            //Update Model with hotel amenities data
            _context.Entry(amenities).State = EntityState.Modified;

            try
            {
                //Save data changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmenitiesExists(id))
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

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

        //Making new amenities
        public async Task<ActionResult<Amenities>> PostAmenities(Amenities amenities)
        {
          if (_context.Amenities_1 == null)
          {
              return Problem("Entity set 'BlueFlameHotelContext.Amenities_1'  is null.");
          }
            _context.Amenities_1.Add(amenities);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmenities", new { id = amenities.ID }, amenities);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenities(int id)
        {
            if (_context.Amenities_1 == null)
            {
                return NotFound();
            }
            var amenities = await _context.Amenities_1.FindAsync(id);
            if (amenities == null)
            {
                return NotFound();
            }

            _context.Amenities_1.Remove(amenities);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AmenitiesExists(int id)
        {
            return (_context.Amenities_1?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
