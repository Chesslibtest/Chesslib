using ChessLib.Application.Models.DTOs.Stats;
namespace ChessLib.Application.Interfaces;

public interface IStatisticService
{
    Task<UserSkillStatsHistoryDto> GetUserSkillStatsHistoryAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<FavoriteOpeningDto>> GetUserFavoriteOpeningsAsync(Guid userId, CancellationToken cancellationToken);
}