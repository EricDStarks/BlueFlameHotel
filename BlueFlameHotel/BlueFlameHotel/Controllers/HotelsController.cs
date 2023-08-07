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
using BlueFlameHotel.Models.Services;

namespace BlueFlameHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly BlueFlameHotelContext _context;
        private readonly IHotel _hotel;

        public HotelsController(BlueFlameHotelContext context, IHotel hotel)
        {
            _context = context;
            _hotel = hotel;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel()
        {
            return await _hotel.GetHotel();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            return await  _hotel.GetHotel(id);
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
            await _hotel.PutHotel(id, hotel);

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]


        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
          
          await _hotel.PostHotel(hotel);
            return CreatedAtAction("GetHotel", new { id = hotel.ID }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            _hotel.DeleteHotel(id);
            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return _hotel.HotelExists(id);
        }
    }
}
