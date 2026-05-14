using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ChessLib.Domain.ValueObjects;


namespace ChessLib.Infrastructure.Converters;

    public class EcoCodeConverter : ValueConverter<EcoCode , string>
    {
        public EcoCodeConverter() : base(
            v => v.Value, 
            v => new EcoCode(v)) 
        {
        }
    }