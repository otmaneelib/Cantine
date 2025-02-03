using AutoMapper;
using Core.Dtos;
using Core.Entities;

namespace Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Client mappings
            CreateMap<Client, ClientDTO>().ReverseMap();

            // Meal mappings
            CreateMap<Meal, MealDTO>().ReverseMap();

            // Supplement mappings
            CreateMap<Supplement, SupplementDTO>().ReverseMap();

            // Ticket mappings
            CreateMap<Ticket, TicketDTO>().ReverseMap();
        }
    }
}
