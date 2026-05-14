namespace ChessLib.Application.Models.DTOs.Stats;

public record UserSkillStatsHistoryDto
(
    int TotalGames,
    int Wins,
    int Losses,
    int Draws,
    double WinRate,
    double AverageAccuracy
);