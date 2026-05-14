using MediatR;
using ChessLib.Application.Models.DTOs;

namespace ChessLib.Application.Features.Openings.Queries.GetOpeningVariants;

public record GetOpeningVariantsQuery(Guid OpeningId) : IRequest<IEnumerable<OpeningVariantDto>>;