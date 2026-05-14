using ChessLib.Domain.ValueObjects;

namespace ChessLib.Domain.Entities;

    public class Opening
    {
        public Guid Id{get; init;}
        public string Name {get; private set;} = string.Empty;
        public EcoCode EcoCode {get; private set;} = null!;
        public Fen CurrentFen {get; private set;} = null!;
        public string Moves {get; private set;} = string.Empty;
        public string Description {get; private set;} = string.Empty;

        public ICollection<OpeningVariant> Variants {get; private set;} = new List<OpeningVariant>();

        protected Opening (){}

// 
// Конструктор который делает новый дебют, который может быть использован для создания новых вариантов этого дебюта.
// 
        public Opening(Guid id, string name, EcoCode ecoCode, Fen currentFen, string moves, string description)
        {
            Id = id;
            Name = name;
            EcoCode = ecoCode;
            CurrentFen = currentFen;
            Moves = moves;
            Description = description;
        }

        public void AddVariant(String name, string moves, Fen targetFen, string description)
        {
            var variant = new OpeningVariant(Guid.NewGuid(), this.Id, name, moves, targetFen, description);
            Variants.Add(variant);
    }
}
