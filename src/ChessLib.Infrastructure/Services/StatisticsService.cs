using ChessLib.Application.Models.DTOs.Stats;
using ChessLib.Application.Interfaces;
using ChessLib.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace ChessLib.Infrastructure.Services;

public class StatisticsService : IStatisticService
{
    private readonly ApplicationDbContext _context;

    public StatisticsService(ApplicationDbContext context)
    {
        _context = context;
    }

// 
// Сервис для получения статистики пользователя, включая историю навыков и любимые дебюты. 
// 
    public async Task<UserSkillStatsHistoryDto> GetUserSkillStatsHistoryAsync(Guid userId, CancellationToken cancellationToken){
          var game = await _context.Games
            .AsNoTracking()
            .Where(g => g.WhitePlayerId == userId || g.BlackPlayerId == userId)
            .Select(g => new {g.Result , g.WhitePlayerId })
            .ToListAsync(cancellationToken);

            if(!game.Any())
            {
                return new UserSkillStatsHistoryDto(0, 0, 0, 0, 0, 0);
            }

            var totalGames = game.Count;
            var wins = game.Count(g => g.Result == GameResult.WhiteWins|| g.Result == GameResult.BlackWins);
            var losses = game.Count(g => (g.WhitePlayerId == userId && g.Result == GameResult.BlackWins) || (g.WhitePlayerId != userId && g.Result == GameResult.WhiteWins));
            var draws = game.Count(g => g.Result == GameResult.Draw);

            double winRate = totalGames > 0 ? (double)wins / totalGames * 100 : 0;
            double averageAccuracy = Math.Round(game.Average(g => g.Result == GameResult.WhiteWins || g.Result == GameResult.BlackWins ? 1 : 0) * 100, 1);

          
          return new UserSkillStatsHistoryDto(totalGames, wins, losses, draws, winRate, averageAccuracy);
    }

        public async Task<IEnumerable<FavoriteOpeningDto>> GetUserFavoriteOpeningsAsync(Guid userId, CancellationToken cancellationToken){
                return await _context.Games
                    .AsNoTracking()
                    .Where(g => (g.WhitePlayerId == userId || g.BlackPlayerId == userId) && g.OpeningName != null && g.EcoCode != null)

                    .GroupBy(g => new { g.OpeningName, g.EcoCode })
                    .Select(g => new FavoriteOpeningDto(
                        g.Key.OpeningName!,
                        g.Key.EcoCode!,
                        g.Count(),
                        Math.Round((double)g.Count(g => (g.WhitePlayerId == userId && g.Result == GameResult.WhiteWins) || (g.BlackPlayerId == userId && g.Result == GameResult.BlackWins)) / g.Count() * 100, 1),
                        ""
                    ))
                    .OrderByDescending(o => o.TimesPlayed)
                    .Take(5)
                    .ToListAsync(cancellationToken);
        }
}