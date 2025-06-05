using AutoMapper;
using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Models.DTO;

namespace Comfort_Hotel_Api.Mapping
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Room, RoomDTO>().ReverseMap();
            CreateMap<Room, RoomCreateDTO>().ReverseMap();
            CreateMap<Room, RoomUpdateDTO>().ReverseMap();


            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Booking, BookingCreateDto>().ReverseMap();


            CreateMap<CartItem,CreatecartDto>().ReverseMap();
            CreateMap<CartItem, CartResponseDto>().ReverseMap();


            CreateMap<RoomsRate, RoomRateDto>().ReverseMap();
            CreateMap<RoomsRate, RoomRateCreateDto>().ReverseMap();


            CreateMap<ApplicationUser, LocalUser>().ReverseMap();

        }

    }
}
