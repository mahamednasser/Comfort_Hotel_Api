using System.ComponentModel.DataAnnotations;

namespace Comfort_Hotel_Api.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public decimal PricePerNight { get; set; }
        public String Details { get; set; }
        public int Space { get; set; }

        public int Capacity { get; set; }
        public double Rate { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
       

        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }



    }
}
