using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlueFlameHotel.Data;
using BlueFlameHotel.Models;

namespace BlueFlameHotel.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIHotelsController : ControllerBase
    {
        private readonly BlueFlameHotelContext _context;

        public APIHotelsController(BlueFlameHotelContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetBlueFlameHotel()
        {
          if (_context.BlueFlameHotel == null)
          {
              return NotFound();
          }
            return await _context.BlueFlameHotel.ToListAsync();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
          if (_context.BlueFlameHotel == null)
          {
              return NotFound();
          }
            var hotel = await _context.BlueFlameHotel.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.ID)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
          if (_context.BlueFlameHotel == null)
          {
              return Problem("Entity set 'BlueFlameHotelContext.BlueFlameHotel'  is null.");
          }
            _context.BlueFlameHotel.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.ID }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (_context.BlueFlameHotel == null)
            {
                return NotFound();
            }
            var hotel = await _context.BlueFlameHotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.BlueFlameHotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return (_context.BlueFlameHotel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
