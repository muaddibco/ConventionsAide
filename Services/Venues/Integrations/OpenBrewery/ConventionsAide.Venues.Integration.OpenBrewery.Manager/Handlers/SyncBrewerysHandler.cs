using ConventionsAide.Core.Communication;
using ConventionsAide.Venues.Integration.OpenBrewery.Contracts;
using ConventionsAide.Venues.Integration.OpenBrewery.Manager.Configuration;
using Microsoft.Extensions.Options;
using Flurl;
using Flurl.Http;
using ConventionsAide.Venues.Integration.OpenBrewery.Manager.Model;
using AutoMapper;
using ConventionsAide.Venues.Contracts;
using ConventionsAide.Venues.Client;

namespace ConventionsAide.Venues.Integration.OpenBrewery.Manager.Handlers;

public class SyncBrewerysHandler : ApiHandlerBase<SyncBrewerysRequestDto, SyncBrewerysResponseDto>
{
    private readonly OpenBreweryOptions _openBreweryConfiguration;
    private readonly IMapper _mapper;
    private readonly ICommunicationService _communicationService;

    public SyncBrewerysHandler(IOptions<OpenBreweryOptions> openBreweryConfiguration, IMapper mapper, ICommunicationService communicationService)
    {
        _openBreweryConfiguration = openBreweryConfiguration.Value;
        _mapper = mapper;
        _communicationService = communicationService;
    }

    public override async Task<SyncBrewerysResponseDto> HandleAsync(CommandMessage<SyncBrewerysRequestDto> message, CancellationToken cancellationToken)
    {
        try
        {
            int page = 1;
            int lastCount;
            int totalCount = 0;
            do
            {
                var brewerys = await _openBreweryConfiguration.ListUri
                    .SetQueryParam("per_page", _openBreweryConfiguration.PerPage)
                    .SetQueryParam("page", page++)
                    .GetJsonAsync<BreweryDto[]>();
                lastCount = brewerys.Length;
                totalCount += lastCount;
                await Parallel
                    .ForEachAsync(
                        brewerys.Select(b => _mapper.Map<BreweryDto, UpdateVenueInBatchRequestDto>(b)),
                        async (v, ct) => await _communicationService.UpdateVenuesInBatchAsync(v));
            } while (lastCount == _openBreweryConfiguration.PerPage);

            return new SyncBrewerysResponseDto { TotalCount = totalCount };

        }
        catch (Exception ex)
        {
            throw;
        }    
    }
}
