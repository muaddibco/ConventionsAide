using AutoMapper;
using ConventionsAide.Venues.Contracts;
using ConventionsAide.Venues.Integration.OpenBrewery.Manager.Model;

namespace ConventionsAide.Venues.Integration.OpenBrewery.Manager
{
    public class OpenBreweryAutoMapperProfile : Profile
    {
        public OpenBreweryAutoMapperProfile()
        {
            CreateMap<BreweryDto, UpdateVenueInBatchRequestDto>()
                .ForMember(t => t.ExternalId, d => d.MapFrom(dto => dto.Id))
                .ForMember(t => t.Address1, d => d.MapFrom(dto => dto.Street))
                .ForMember(t => t.ContactPhone, d => d.MapFrom(dto => dto.Phone))
                .ForMember(t => t.WebUrl, d => d.MapFrom(dto => dto.WebsiteUrl))
                .ForMember(t => t.VenueType, d => d.MapFrom(dto => "brewery"));
        }
    }
}
