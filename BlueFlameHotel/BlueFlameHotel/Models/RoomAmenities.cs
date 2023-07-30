using System.ComponentModel.DataAnnotations;

namespace BlueFlameHotel.Models
{
    public class RoomAmenities
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public int RoomsID { get; set; }
        
        [Required]
        public int AmenityID { get; set;}
    }
}
