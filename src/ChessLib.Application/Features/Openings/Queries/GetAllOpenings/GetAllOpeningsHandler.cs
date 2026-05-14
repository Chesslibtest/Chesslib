using MediatR;
using ChessLib.Application.Models.DTOs;
using ChessLib.Application.Interfaces;


namespace ChessLib.Application.Features.Openings.Queries.GetAllOpenings;

public class GetAllOpeningsHandler : IRequestHandler<GetAllOpeningsQuery , IEnumerable<OpeningLookupDto>>
{
    private readonly IOpeningService _openingService;

    public GetAllOpeningsHandler(IOpeningService openingService)
    {
        _openingService = openingService;
    }

    public async Task<IEnumerable<OpeningLookupDto>> Handle(GetAllOpeningsQuery request, CancellationToken cancellationToken)
    {
        return await _openingService.GetAllOpeningsAsync(cancellationToken);
    }
}