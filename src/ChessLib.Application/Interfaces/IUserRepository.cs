using ChessLib.Domain.Entities;
using ChessLib.Domain.ValueObjects;
namespace ChessLib.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id , CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(Email email , CancellationToken cancellationToken);
    Task<bool> IsEmailUniqueAsync(Email email , CancellationToken cancellationToken);
    Task AddAsync(User user , CancellationToken cancellationToken);
    void UpdateAsync(User user);
}