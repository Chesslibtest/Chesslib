using ChessLib.Application.Interfaces;
using ChessLib.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChessLib.Infrastructure.Persistence.Repositories;

public class GameRepository : IGameRepository
{
    private readonly ApplicationDbContext _context;
    public GameRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Games
            .Include(g => g.WhitePlayer)
            .Include(g => g.BlackPlayer)
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task<Game?> GetActiveGameByPlayerIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Games
            .Include(g => g.WhitePlayer)
            .Include(g => g.BlackPlayer)
            .FirstOrDefaultAsync(g =>
                (g.WhitePlayerId == userId || g.BlackPlayerId == userId) && g.Result == GameResult.InProgress, cancellationToken);  
    }

    public async Task<IEnumerable<Game>> GetFinishedGameByPlayerIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Games
            .Include(g => g.WhitePlayer)
            .Include(g => g.BlackPlayer)
            .Where(g =>
                (g.WhitePlayerId == userId || g.BlackPlayerId == userId) && g.Result != GameResult.InProgress)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Game game , CancellationToken cancellationToken)
    {
        await _context.Games
             .AddAsync(game, cancellationToken);
    }

    public void Update(Game game)
    {
        _context.Games.Update(game);
    }
}