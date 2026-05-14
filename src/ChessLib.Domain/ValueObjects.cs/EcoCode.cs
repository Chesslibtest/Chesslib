using System.Text.RegularExpressions;
namespace ChessLib.Domain.ValueObjects;

public record EcoCode
{
    public string Value{get; init;}

    public EcoCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[A-E]\d{2}$", RegexOptions.IgnoreCase))
        {
            throw new ArgumentException("ECO code must be a letter (A-E) followed by two digits (e.g., 'C60').");
        }
        Value = value.ToUpper();
    }
}