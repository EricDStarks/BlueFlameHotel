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
    public class RoomAmenitiesController : ControllerBase
    {
        private readonly BlueFlameHotelContext _context;

        public RoomAmenitiesController(BlueFlameHotelContext context)
        {
            _context = context;
        }

        // GET: api/RoomAmenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomAmenities>>> GetRoomAmenities_1()
        {
          if (_context.RoomAmenities_1 == null)
          {
              return NotFound();
          }
            return await _context.RoomAmenities_1.ToListAsync();
        }

        // GET: api/RoomAmenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomAmenities>> GetRoomAmenities(int id)
        {
          if (_context.RoomAmenities_1 == null)
          {
              return NotFound();
          }
            var roomAmenities = await _context.RoomAmenities_1.FindAsync(id);

            if (roomAmenities == null)
            {
                return NotFound();
            }

            return roomAmenities;
        }

        // PUT: api/RoomAmenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomAmenities(int id, RoomAmenities roomAmenities)
        {
            if (id != roomAmenities.ID)
            {
                return BadRequest();
            }

            _context.Entry(roomAmenities).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomAmenitiesExists(id))
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

        // POST: api/RoomAmenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomAmenities>> PostRoomAmenities(RoomAmenities roomAmenities)
        {
          if (_context.RoomAmenities_1 == null)
          {
              return Problem("Entity set 'BlueFlameHotelContext.RoomAmenities_1'  is null.");
          }
            _context.RoomAmenities_1.Add(roomAmenities);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomAmenities", new { id = roomAmenities.ID }, roomAmenities);
        }

        // DELETE: api/RoomAmenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomAmenities(int id)
        {
            if (_context.RoomAmenities_1 == null)
            {
                return NotFound();
            }
            var roomAmenities = await _context.RoomAmenities_1.FindAsync(id);
            if (roomAmenities == null)
            {
                return NotFound();
            }

            _context.RoomAmenities_1.Remove(roomAmenities);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomAmenitiesExists(int id)
        {
            return (_context.RoomAmenities_1?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
