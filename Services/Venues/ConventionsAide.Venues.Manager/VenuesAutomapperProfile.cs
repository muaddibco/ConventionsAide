using AutoMapper;
using ConventionsAide.Venues.Contracts;
using ConventionsAide.Venues.Domain;

namespace ConventionsAide.Venues.Manager
{
    public class VenuesAutomapperProfile : Profile
    {
        public VenuesAutomapperProfile()
        {
            CreateMap<Venue, VenueDto>();
            CreateMap<UpdateVenueInBatchRequestDto, Venue>();
        }
    }
}
