using System.ComponentModel.DataAnnotations;

namespace BlueFlameHotel.Models
{
    public class RoomAmenities
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public int RoomsID { get; set; }
        
 
        public int AmenityID { get; set;}

     

     

        public List<Room>? Rooms { get; set; }
        public List<Amenities>? Amenity { get; set; }
    }
}
