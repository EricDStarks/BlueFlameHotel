using System.ComponentModel.DataAnnotations;

namespace BlueFlameHotel.Models
{
    public class Amenities
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
