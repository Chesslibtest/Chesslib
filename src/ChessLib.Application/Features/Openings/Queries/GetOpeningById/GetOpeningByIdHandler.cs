using ChessLib.Application.Interfaces;
using ChessLib.Application.Models.DTOs;
using MediatR;

namespace ChessLib.Application.Features.Openings.Queries.GetOpeningById;

public class GetOpeningByIdHandler : IRequestHandler<GetOpeningByIdQuery, OpeningDetailDto?>
{
    private readonly IOpeningService _openingService;

    public GetOpeningByIdHandler(IOpeningService openingService)
    {
        _openingService = openingService;
    }

    public async Task<OpeningDetailDto?> Handle(GetOpeningByIdQuery request, CancellationToken cancellationToken)
    {
        return await _openingService.GetOpeningDetailsAsync(request.OpeningId ,cancellationToken);
    }
}