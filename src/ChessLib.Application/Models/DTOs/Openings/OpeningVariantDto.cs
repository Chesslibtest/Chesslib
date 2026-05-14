namespace ChessLib.Application.Models.DTOs;

public record OpeningVariantDto
(
    Guid Id,
    string Name,
    string Moves,
    string Description,
    string TargetFen
);