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
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, Client>();

            // Meal mappings
            CreateMap<Meal, MealDTO>();
            CreateMap<MealDTO, Meal>();

            // Supplement mappings
            CreateMap<Supplement, SupplementDTO>();
            CreateMap<SupplementDTO, Supplement>();

            // Ticket mappings
            CreateMap<Ticket, TicketDTO>();
            CreateMap<TicketDTO, Ticket>();
        }
    }
}
