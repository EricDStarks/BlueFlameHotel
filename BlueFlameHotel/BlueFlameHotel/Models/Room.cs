using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueFlameHotel.Models
{
    public class Room
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]  
        public int Layout { get; set; }

        public List<HotelRoom>? HotelRooms { get; set; }

        [NotMapped]
        public List<RoomAmenities>? RoomAmenities { get; set; }  
    }
}
