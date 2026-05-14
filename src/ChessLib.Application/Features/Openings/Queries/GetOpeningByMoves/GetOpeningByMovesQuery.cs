using MediatR;
using ChessLib.Application.Models.DTOs;

namespace ChessLib.Application.Features.Openings.Queries.GetOpeningByMoves;

public record GetOpeningByMovesQuery(string Moves) : IRequest<OpeningDetailDto?>;