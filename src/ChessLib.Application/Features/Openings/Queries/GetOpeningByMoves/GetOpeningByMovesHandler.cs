using MediatR;
using ChessLib.Application.Models.DTOs;
using ChessLib.Application.Interfaces;

namespace ChessLib.Application.Features.Openings.Queries.GetOpeningByMoves;

public class GetOpeningByMovesHandler : IRequestHandler<GetOpeningByMovesQuery , OpeningDetailDto?>
{
    private readonly IOpeningService _openingService;

    public GetOpeningByMovesHandler(IOpeningService openingService)
    {
        _openingService = openingService;
    }

    public async Task<OpeningDetailDto?> Handle (GetOpeningByMovesQuery request , CancellationToken cancellationToken)
    {
        return await _openingService.GetByMovesAsync(request.Moves , cancellationToken);
    }
}