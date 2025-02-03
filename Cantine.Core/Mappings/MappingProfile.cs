using AutoMapper;
using Cantine.Core.Dtos;
using Cantine.Core.Entities;

namespace Cantine.Core.Mappings
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
