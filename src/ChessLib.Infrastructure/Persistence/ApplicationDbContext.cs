using Microsoft.EntityFrameworkCore;
using ChessLib.Domain.Entities;
using ChessLib.Domain.ValueObjects;
using ChessLib.Infrastructure.Converters;

namespace ChessLib.Infrastructure.Persistence;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

// 
// Конфигурация конвенций для Value Objects.
// 
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<EcoCode>()
            .HaveConversion<EcoCodeConverter>();

        configurationBuilder
            .Properties<Fen>()
            .HaveConversion<FenConverter>();

        configurationBuilder
            .Properties<Email>()
            .HaveConversion<EmailConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<Profile>(p => p.UserId);

        modelBuilder.Entity<Opening>()
             .HasMany(o => o.Variants)
             .WithOne(v => v.Opening)
             .HasForeignKey(v => v.OpeningId)
             .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasOne(g => g.WhitePlayer)
                .WithMany()
                .HasForeignKey(g => g.WhitePlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(g => g.BlackPlayer)
                .WithMany()
                .HasForeignKey(g => g.BlackPlayerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

        public DbSet<Game> Games { get; set;}
        public DbSet<Opening> Openings { get; set;}
        public DbSet<OpeningVariant> OpeningVariants { get; set;}
        public DbSet<Profile> Profiles { get; set;}
        public DbSet<User> Users { get; set;}
    }