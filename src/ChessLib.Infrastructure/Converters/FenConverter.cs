using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ChessLib.Domain.ValueObjects;


namespace ChessLib.Infrastructure.Converters;

    public class FenConverter : ValueConverter<Fen , string>
    {
        public FenConverter() : base(
            v => v.Value, 
            v => new Fen(v)) 
        {
        }
    }