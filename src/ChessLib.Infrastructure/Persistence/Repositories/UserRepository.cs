using ChessLib.Application.Interfaces;
using ChessLib.Domain.Entities;
using ChessLib.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ChessLib.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id , CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(Email email , CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users
             .AddAsync(user, cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken)
    {
        return !await _context.Users
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    public void UpdateAsync(User user)
    {
        _context.Users.Update(user);
    }
}

