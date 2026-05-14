using ChessLib.Domain.ValueObjects;

namespace ChessLib.Domain.Entities;

public class Game
{
    public Guid Id {get; init;}
    public Guid WhitePlayerId {get; private set;}
    public Guid BlackPlayerId {get; private set;}

    public string Moves {get; private set;} = string.Empty;
    public Fen Fen {get; private set;} = null!;
    public GameResult Result {get; private set;} = GameResult.InProgress;

    public User WhitePlayer {get; private set;} = null!;
    public User BlackPlayer {get; private set;} = null!;

    public string? OpeningName {get; private set;}
    public string? EcoCode {get; private set;}

    protected Game (){}

    public Game(Guid id, Guid whitePlayerId, Guid blackPlayerId, string moves, Fen fen, GameResult result)
    {
        Id = id;
        WhitePlayerId = whitePlayerId;
        BlackPlayerId = blackPlayerId;
        Moves = moves ?? string.Empty;
        Fen = fen ?? throw new ArgumentNullException(nameof(fen));
        Result = result;
    }

    public void MakeMove(string move , Fen newFen)
    {
        if(Result != GameResult.InProgress)
        {
            throw new InvalidOperationException("Game is already finished.");
        }
        if(string.IsNullOrEmpty(move))
        {
            throw new ArgumentException("Move cannot be null or empty.", nameof(move));
        }

        Moves = string.IsNullOrEmpty(Moves) 
        ? move : $"{Moves} {move}";
        Fen = newFen ?? throw new ArgumentNullException(nameof(newFen));
    }

    public void SetResult(GameResult finishedResult)
    {
        if(Result != GameResult.InProgress)
        {
            throw new InvalidOperationException("Game is already finished.");
        }
        Result = finishedResult;
    }
    public void SetOpeningInfo(string openingName, string ecoCode)
    {
        OpeningName = openingName;
        EcoCode = ecoCode;
    }
}