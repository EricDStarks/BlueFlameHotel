using System.ComponentModel.DataAnnotations;

namespace BlueFlameHotel.Models
{
    public class HotelRoom
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public int RoomID { get; set; }
        
        [Required]
        public int HotelID { get; set; }
        
        [Required]
        public double Price { get; set; }

        //Hotel navigation properties
        public Hotel Hotel { get; set; }
        public Room room { get; set; }

    }
}
