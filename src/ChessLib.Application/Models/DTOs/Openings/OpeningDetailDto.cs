namespace ChessLib.Application.Models.DTOs;

public record OpeningDetailDto
(
    Guid Id,
    string Name,
    string EcoCode,
    string CurrentFen,
    string Moves,
    string Description,
    IEnumerable<OpeningVariantDto> Variants
);