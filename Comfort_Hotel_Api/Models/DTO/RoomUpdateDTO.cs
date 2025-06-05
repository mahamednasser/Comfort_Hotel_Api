using System.ComponentModel.DataAnnotations;

namespace Comfort_Hotel_Api.Models.DTO
{
    public class RoomUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Number { get; set; }
        public decimal PricePerNight { get; set; }
        public String Details { get; set; }
        [Required]
        public int Space { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]

        public double Rate { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        public IFormFile? Image { get; set; }


    }
}
