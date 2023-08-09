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

        public Room Room { get; set; }

       public Amenities Amenities { get; set; }
    }
}
