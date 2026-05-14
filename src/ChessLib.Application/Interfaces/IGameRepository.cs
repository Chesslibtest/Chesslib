using ChessLib.Domain.Entities;

namespace ChessLib.Application.Interfaces;

public interface IGameRepository
{
    Task<Game?> GetByIdAsync (Guid id , CancellationToken cancellationToken);
    Task<Game?> GetActiveGameByPlayerIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<Game>> GetFinishedGameByPlayerIdAsync(Guid userId, CancellationToken cancellationToken);
    Task AddAsync(Game game, CancellationToken cancellationToken);
    void Update(Game game);
}