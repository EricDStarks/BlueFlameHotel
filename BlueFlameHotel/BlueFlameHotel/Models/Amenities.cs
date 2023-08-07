using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueFlameHotel.Models
{
    public class Amenities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Required]
        public string Name { get; set; }

        public List<RoomAmenities> RoomAmenities { get; set; }
    }
}
