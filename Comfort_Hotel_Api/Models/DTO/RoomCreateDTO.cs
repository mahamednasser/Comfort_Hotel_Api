using System.ComponentModel.DataAnnotations;

namespace Comfort_Hotel_Api.Models.DTO
{
    public class RoomCreateDTO
    {
        
        [Required]
        public string Name { get; set; }
        public String Details { get; set; }
        public int Number { get; set; }
        public decimal PricePerNight { get; set; }

        public int Space { get; set; }

        public int Capacity { get; set; }
        [Required]

        public double Rate { get; set; }

        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        
    }
}
