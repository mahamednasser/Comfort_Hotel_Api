namespace Comfort_Hotel_Api.Models.DTO
{
    public class RoomRateCreateDto
    {
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
        public int NumOfStars { get; set; }
        public int RoomId { get; set; }
    }
}
