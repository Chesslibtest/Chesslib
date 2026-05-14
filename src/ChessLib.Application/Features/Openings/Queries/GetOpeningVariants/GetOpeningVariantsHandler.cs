using MediatR;
using ChessLib.Application.Models.DTOs;
using ChessLib.Application.Interfaces;

namespace ChessLib.Application.Features.Openings.Queries.GetOpeningVariants;

public class GetOpeningVariantsHandler : IRequestHandler<GetOpeningVariantsQuery, IEnumerable<OpeningVariantDto>>
{
    private readonly IOpeningService _openingService;

    public GetOpeningVariantsHandler(IOpeningService openingService)
    {
        _openingService = openingService;
    }

    public async Task<IEnumerable<OpeningVariantDto>> Handle(GetOpeningVariantsQuery request, CancellationToken cancellationToken)
    {
        return await _openingService.GetOpeningVariantsAsync(request.OpeningId, cancellationToken);
    }

    
}

