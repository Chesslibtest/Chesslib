using ChessLib.Application.Interfaces;
using ChessLib.Application.Models.DTOs;
using ChessLib.Domain.Entities;
using ChessLib.Domain.ValueObjects;

namespace ChessLib.Infrastructure.Services;

public class  JsonOpeningService : IOpeningService
{
    private readonly string _jsonService;

    public JsonOpeningService(string jsonService)
    {
        _jsonService = jsonService;
    }

    public async Task<IEnumerable<OpeningLookupDto>> GetAllOpeningsAsync(CancellationToken cancellationToken)
    {
        var openings = await LoadOpeningsAsync(cancellationToken);
        return openings.Select(o => new OpeningLookupDto(
            o.Id,
            o.Name,
            o.EcoCode.Value
        ));
    }
    
    public async Task<OpeningDetailDto?> GetOpeningDetailsAsync(Guid openingId, CancellationToken cancellationToken)
    {
        var openings = await LoadOpeningsAsync(cancellationToken);
        var opening = openings.FirstOrDefault(o => o.Id == openingId);
        return opening != null ? MapToOpeningDetailDto(opening) : null;


    }

    public async Task<OpeningDetailDto?> GetByMovesAsync(string moves, CancellationToken cancellationToken)
    {
        var openings = await LoadOpeningsAsync(cancellationToken);
        var opening = openings.FirstOrDefault(o => o.Moves == moves);
        return opening != null ? MapToOpeningDetailDto(opening) : null;
    }

    public async Task<IEnumerable<OpeningVariantDto>> GetOpeningVariantsAsync(Guid openingId, CancellationToken cancellationToken)
    {
        var openings = await LoadOpeningsAsync(cancellationToken);
        var opening = openings.FirstOrDefault(o => o.Id == openingId);

        if (opening == null)
        {
            return Enumerable.Empty<OpeningVariantDto>();
        }

        return opening.Variants.Select(v => new OpeningVariantDto(
            v.Id,
            v.Name,
            v.Moves,
            v.Description,
            v.TargetFen.Value
        ));

    }

    private async Task<List<Opening>> LoadOpeningsAsync(CancellationToken cancellationToken)
    {
           if(!File.Exists(_jsonService)) return new List<Opening>();

           var json = await File.ReadAllTextAsync(_jsonService, cancellationToken);
           var options = new System.Text.Json.JsonSerializerOptions
           {
               PropertyNameCaseInsensitive = true
           };
           var models = System.Text.Json.JsonSerializer.Deserialize<List<OpeningJsonModel>>(json, options);
           if (models == null) return new List<Opening>();

           var openings = new List<Opening>();
              foreach (var model in models)
              {
                try
                {
                    var opening = new Opening(
                         model.Id,
                         model.Name,
                         new EcoCode(model.EcoCode),
                         new Fen(model.CurrentFen),
                         model.Moves,
                         model.Description
                    );
        
                    foreach (var variant in model.Variants)
                    {
                        opening.AddVariant(
                            variant.Name,
                            variant.Moves,
                            new Fen(variant.TargetFen),
                            variant.Description
                        );
                    }
        
                    openings.Add(opening);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deserializing opening with ID {model.Id}: {ex.Message}");
                }
               
    }
    return openings;
}

    private OpeningDetailDto MapToOpeningDetailDto(Opening opening)
    {
        return new OpeningDetailDto(
            opening.Id,
            opening.Name,
            opening.EcoCode.Value,   
            opening.CurrentFen.Value, 
            opening.Moves,
            opening.Description,
            opening.Variants.Select(v => new OpeningVariantDto(
            v.Id,
            v.Name,
            v.Moves,
            v.Description,
            v.TargetFen.Value     
        ))
        );
    }

    private class OpeningJsonModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string EcoCode { get; set; } = ""; 
        public string CurrentFen { get; set; } = ""; 
        public string Moves { get; set; } = "";
        public string Description { get; set; } = "";
        public List<VariantJsonModel> Variants { get; set; } = new();
    }

    private class VariantJsonModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Moves { get; set; } = "";
        public string TargetFen { get; set; } = ""; 
        public string Description { get; set; } = "";
    }
}