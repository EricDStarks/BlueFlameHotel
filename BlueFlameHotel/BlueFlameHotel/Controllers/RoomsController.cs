using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlueFlameHotel.Data;
using BlueFlameHotel.Models;
using BlueFlameHotel.Models.Interfaces;

namespace BlueFlameHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly BlueFlameHotelContext _hotel;
        private readonly IHotel context;

        public RoomsController(BlueFlameHotelContext context) 
        {
            _hotel = context;
                       
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoom()
        {
            if (_hotel.Rooms == null)
            {
                return NotFound();
            }
            return _hotel.Rooms.ToList();
            //return await _context.Room.ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            if (_hotel.Rooms == null)
            {
                return NotFound();
            }
            var room = await _hotel.Rooms.FindAsync(id);

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
            _hotel.Entry(room).State = EntityState.Modified;

            try
            {
                //Save data changes
                await _hotel.SaveChangesAsync();
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
            if (_hotel.Rooms == null)
            {
                return Problem("Entity set 'BlueFlameHotelContext.Room'  is null.");
            }
            _hotel.Rooms.Add(room);
            await _hotel.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = room.ID }, room);
        }

        [HttpPost]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> PostAmenitiesToRoom(int AmenitiesID, int roomID)
        {
            if (_hotel.RoomAmenities == null)
            {
                return Problem("Entity set 'BlueFlameHotelContext.Room'  is null.");
            }
            var amenities = await _hotel.Amenities.FindAsync(AmenitiesID);
            if (amenities == null)
            {
                return Problem("No amenities with that ID exist");
            }
            var room = _hotel.Rooms.FindAsync(roomID);
            RoomAmenities roomAmenities = new RoomAmenities();
            if (room == null)
            {
                return Problem("No room with that ID exist");
            }
            RoomAmenities newRA;

            try
            {
                newRA = _hotel.RoomAmenities.Add(new RoomAmenities { RoomsID = roomID }).Entity;
            }
            catch (Exception e)
            {

            }
            finally
            {
                await _hotel.SaveChangesAsync();
            }
            return CreatedAtAction("Post Amenities to room", RoomExists) ;
        }

            // DELETE: api/Rooms/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteRoom(int id)
            {
                if (_hotel.Rooms == null)
                {
                    return NotFound();
                }
                var room = await _hotel.Rooms.FindAsync(id);
                if (room == null)
                {
                    return NotFound();
                }

                _hotel.Rooms.Remove(room);
                await _hotel.SaveChangesAsync();

                return NoContent();
            }

            private bool RoomExists(int id)
            {
                return (_hotel.Rooms?.Any(e => e.ID == id)).GetValueOrDefault();
            }
        }
    } 
