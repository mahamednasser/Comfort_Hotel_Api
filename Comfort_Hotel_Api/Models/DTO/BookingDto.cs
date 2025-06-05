using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comfort_Hotel_Api.Models.DTO
{
    public class BookingDto
    {
        public int Id { get; set; }
        public decimal price { get; set; }

        public DateTime creationDate { get; set; }
        [Required(ErrorMessage = "The Start date Is Requiered")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "The End date Is Requiered")]
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
        public int RoomId { get; set; }
      
    }
}
