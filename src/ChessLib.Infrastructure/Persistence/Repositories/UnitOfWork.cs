using System.Runtime.CompilerServices;
using ChessLib.Application.Interfaces;

namespace ChessLib.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IUserRepository Users { get; private set;}
    public IGameRepository Games { get; private set;}
    public UnitOfWork(ApplicationDbContext context , IUserRepository userRepository, IGameRepository gameRepository)
    {
        _context = context;
        Users = userRepository;
        Games = gameRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
