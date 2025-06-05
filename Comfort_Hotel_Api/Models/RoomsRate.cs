using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comfort_Hotel_Api.Models
{
    public class RoomsRate
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
        public int NumOfStars { get; set; }

        public string UserId { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room? room { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? user { get; set; }
    }
}
