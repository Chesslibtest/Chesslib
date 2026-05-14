namespace ChessLib.Application.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IGameRepository Games { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}