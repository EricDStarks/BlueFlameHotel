using System.ComponentModel.DataAnnotations;

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

        [Required]
        public List<HotelRoom> HotelRooms { get; set; }

        [Required]
        public List<Amenities> Amenities { get; set; }  
    }
}
