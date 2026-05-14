namespace ChessLib.Application.Models.DTOs.Stats;

public record FavoriteOpeningDto
(
    string OpeningName,
    string EcoCode,
    int TimesPlayed,
    double WinRate,
    string PerfomanceColor
);
