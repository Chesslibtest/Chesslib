using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ChessLib.Domain.ValueObjects;


namespace ChessLib.Infrastructure.Converters;

    public class EmailConverter : ValueConverter<Email , string>
    {
        public EmailConverter() : base(
            v => v.Value, 
            v => new Email(v)) 
        {
        }
    }