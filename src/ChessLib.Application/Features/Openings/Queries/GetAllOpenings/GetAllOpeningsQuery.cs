using MediatR;
using ChessLib.Application.Models.DTOs;

namespace ChessLib.Application.Features.Openings.Queries.GetAllOpenings;

public record GetAllOpeningsQuery : IRequest<IEnumerable<OpeningLookupDto>>;