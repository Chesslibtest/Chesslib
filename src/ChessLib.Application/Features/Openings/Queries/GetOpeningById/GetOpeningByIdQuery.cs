using MediatR;
using ChessLib.Application.Models.DTOs;

namespace ChessLib.Application.Features.Openings.Queries.GetOpeningById;

public record GetOpeningByIdQuery(Guid OpeningId) : IRequest<OpeningDetailDto?>;