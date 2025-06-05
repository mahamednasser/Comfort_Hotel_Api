using System.ComponentModel.DataAnnotations.Schema;

namespace Comfort_Hotel_Api.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public DateTime AddingDate { get; set; }
        public string UserId { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room? room { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? user { get; set; }
    }
}
