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
    public class RoomsController : ControllerBase
    {
        private readonly BlueFlameHotelContext _context;

        public RoomsController(BlueFlameHotelContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoom()
        {
            if (_context.Room == null)
            {
                return NotFound();
            }
            return await _context.Room.Include(room => room.HotelRooms).ThenInclude(HotelRoom => HotelRoom.Hotel).Include(room => room.RoomAmenities).ThenInclude(roomamenities => roomamenities.Amenities).ToListAsync();
            //return await _context.Room.ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            if (_context.Room == null)
            {
                return NotFound();
            }
            var room = await _context.Room.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.ID)
            {
                return BadRequest();
            }
            //Update room data (Room name, Layout)
            _context.Entry(room).State = EntityState.Modified;

            try
            {
                //Save data changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (RoomExists(id))
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

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

        //Making new hotel room layouts
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            if (_context.Room == null)
            {
                return Problem("Entity set 'BlueFlameHotelContext.Room'  is null.");
            }
            _context.Room.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = room.ID }, room);
        }

        [HttpPost]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> PostAmenitiesToRoom(int AmenitiesID, int roomID)
        {
            if (_context.RoomAmenities == null)
            {
                return Problem("Entity set 'BlueFlameHotelContext.Room'  is null.");
            }
            var amenities = await _context.Amenities.FindAsync(AmenitiesID);
            if (amenities == null)
            {
                return Problem("No amenities with that ID exist");
            }
            var room = _context.Room.FindAsync(roomID);
            RoomAmenities roomAmenities = new RoomAmenities();
            if (room == null)
            {
                return Problem("No room with that ID exist");
            }
            RoomAmenities newRA;

            try
            {
                newRA = _context.RoomAmenities.Add(new RoomAmenities { Amenities = amenities, RoomsID = roomID }).Entity;
            }
            catch (Exception e)
            {

            }
            finally
            {
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("Post Amenities to room", RoomExists) ;
        }

            // DELETE: api/Rooms/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteRoom(int id)
            {
                if (_context.Room == null)
                {
                    return NotFound();
                }
                var room = await _context.Room.FindAsync(id);
                if (room == null)
                {
                    return NotFound();
                }

                _context.Room.Remove(room);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool RoomExists(int id)
            {
                return (_context.Room?.Any(e => e.ID == id)).GetValueOrDefault();
            }
        }
    } 
