using ChessLib.Application.Models.DTOs;
namespace ChessLib.Application.Interfaces;

public interface IOpeningService
{
    Task<IEnumerable<OpeningLookupDto>> GetAllOpeningsAsync(CancellationToken cancellationToken);
    Task<OpeningDetailDto?> GetOpeningDetailsAsync(Guid openingId, CancellationToken cancellationToken);
    Task<OpeningDetailDto?> GetByMovesAsync(string moves, CancellationToken cancellationToken);
    Task<IEnumerable<OpeningVariantDto>> GetOpeningVariantsAsync(Guid openingId, CancellationToken cancellationToken);

}