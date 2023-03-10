using Duende.IdentityServer.EntityFramework.Options;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using MTR.Domain;

using Action = MTR.Domain.Action;

namespace MTR.DAL;

public class MTRContext : IdentityDbContext<MTRUser, IdentityRole<Guid>, Guid>
{
    public MTRContext(DbContextOptions<MTRContext> options) : base(options)
    {
    }

    public DbSet<Action> Actions { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Cheat> Cheats { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<MuckedCard> MuckedCards { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerCard> PlayerCards { get; set; }
    public DbSet<PlayerPosition> PlayerPositions { get; set; }
    public DbSet<PlayerRemoved> PlayerRemoved { get; set; }
    public DbSet<Round> Rounds { get; set; }
    public DbSet<RoundCard> RoundCards { get; set; }
    public DbSet<RoundReady> RoundReady { get; set; }
    public DbSet<RoundResult> RoundResults { get; set; }
    public DbSet<Turn> Turns { get; set; }
    public DbSet<TurnCard> TurnCards { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Action>().HasAlternateKey(a => new { a.Guid });
        modelBuilder.Entity<Game>().HasAlternateKey(a => new { a.Guid });
        modelBuilder.Entity<Player>().HasAlternateKey(a => new { a.Guid });
        modelBuilder.Entity<PlayerPosition>().HasAlternateKey(a => new { a.Guid });
        modelBuilder.Entity<Round>().HasAlternateKey(a => new { a.Guid });
        modelBuilder.Entity<RoundCard>().HasAlternateKey(a => new { a.Guid });
        modelBuilder.Entity<MTRUser>().HasAlternateKey(a => new { a.Guid });
        modelBuilder.Entity<UserDetail>().HasAlternateKey(a => new { a.Guid });

        SeedCards(modelBuilder);
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Instead of numeric conversion that EFcore uses by default
        configurationBuilder.Properties<Enum>().HaveConversion<string>();
    }

    private void SeedCards(ModelBuilder modelBuilder)
    {
        var id = 0;
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                modelBuilder.Entity<Card>().HasData(new Card() { Id = ++id, Rank = rank, Suit = suit });
            }
        }
    }
}
