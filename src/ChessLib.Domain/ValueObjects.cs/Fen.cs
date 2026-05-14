namespace ChessLib.Domain.ValueObjects;

public record Fen
{
    public string Value {get; init;}

    public Fen(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("FEN string cannot be empty.", nameof(value));
        }
        var parts = value.Split(' ');
        if(parts.Length != 6)
        {
            throw new ArgumentException("FEN string must consist of 6 parts.", nameof(value));
        }
        Value = value;

        
    }
    public bool IsWhiteToMove => Value.Split(' ')[1] == "w";
}