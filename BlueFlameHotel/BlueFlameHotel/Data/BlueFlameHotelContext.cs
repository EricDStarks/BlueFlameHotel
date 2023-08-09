using BlueFlameHotel.Models;
using Microsoft.EntityFrameworkCore;


namespace BlueFlameHotel.Data
{
    public class BlueFlameHotelContext: DbContext
    {
        public DbSet<Amenities> Amenities { get; set; }
        public DbSet<RoomAmenities> RoomAmenities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Hotel> Hotel { get; set; }

        public BlueFlameHotelContext(DbContextOptions<BlueFlameHotelContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tables of information
            //modelBuilder.Entity<Amenities>().HasData(new Amenities { ID = 1, Name = "A/C" });
            //modelBuilder.Entity<Room>().HasData(new Room { ID = 1, Layout = 0,  Name = "Basic Room" }, new Room { ID = 2, Layout = 1, Name = "Basic Single Room"}, new Room { ID = 3, Layout = 2, Name = "Basic Double Room"});
            //modelBuilder.Entity<Hotel>().HasData(new Hotel { ID = 1, Address = "123 Sesame Street", City = "Houston", State = "TX", Name = "Blue Flame Hotel", Phone = "281-330-8004" });

            //Reference Tables
           // modelBuilder.Entity<RoomAmenities>().HasData(new RoomAmenities { ID = 1, AmenityID = 1, RoomsID = 1 });
            //modelBuilder.Entity<HotelRoom>().HasData(new HotelRoom { ID = 1, HotelID =1, RoomID = 1, Price = 189.99 });
        }


    }
}
