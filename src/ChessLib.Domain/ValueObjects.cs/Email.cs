using System.Text.RegularExpressions;

namespace ChessLib.Domain.ValueObjects;

public record Email
{
    public string Value { get; init;}

    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email cannot be empty.", nameof(value));
        }

        var trimmedValue = value.Trim().ToLower();

        if(!EmailRegex.IsMatch(trimmedValue))
        {
            throw new ArgumentException("Invalid email format.", nameof(value));
        }
        Value = trimmedValue;
    }

    public override string ToString() => Value;



}