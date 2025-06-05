using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comfort_Hotel_Api.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public decimal price { get; set; }
        
        public DateTime creationDate { get; set; }
        [Required(ErrorMessage = "The Start date Is Requiered")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage ="The End date Is Requiered")]
        public DateTime EndDate { get; set; }

        public string UserId { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room? room { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? user { get; set; }

    }
}
