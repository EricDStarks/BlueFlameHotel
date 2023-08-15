using BlueFlameHotel.Data;
using BlueFlameHotel.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueFlameHotel.Models.Services
{
    public class HotelService : IHotel
    {
        private BlueFlameHotelContext _context;

        public HotelService(BlueFlameHotelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> DeleteHotel(int id)
        {
            //Former hotel conroller functionality
            var hotel = await _context.BlueFlameHotel.FindAsync(id);
            _context.BlueFlameHotel.Remove(hotel);
            await _context.SaveChangesAsync();
            //End
            return null; //Returning nothing to the controller and no content to the user
        }

        //Get all hotels
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            
            return await _context.BlueFlameHotel.FindAsync();
        }

        public async Task<IEnumerable<Hotel>> GetHotel()
        {
            var hotels = _context.BlueFlameHotel.ToList();
            return hotels;
        }

        public bool HotelExists(int id)
        {
            return _context.BlueFlameHotel.Any(e => e.ID == id);
        }

        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            _context.BlueFlameHotel.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            //Update model with hotel data
            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                //Save data changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return null;
        }
    }
}
