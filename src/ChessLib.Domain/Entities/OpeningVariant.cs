using ChessLib.Domain.ValueObjects;

namespace ChessLib.Domain.Entities;

public class OpeningVariant
{
    public Guid Id {get; init;}
    public Guid OpeningId {get; private set;}

    public string Name {get; private set;} = string.Empty;
    public string Moves {get; private set;} = string.Empty;
    public Fen TargetFen {get; private set;} = null!;

    public string Description {get; private set;} = string.Empty;
    

    public Opening Opening {get; private set;} = null!;

    protected OpeningVariant (){}

    public OpeningVariant(Guid id, Guid openingId, string name, string moves, Fen targetFen, string description)
    {
        Id = id;
        OpeningId = openingId;
        Name = name;
        Moves = moves;
        TargetFen = targetFen;
        Description = description;
    }
}