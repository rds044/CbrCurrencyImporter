using CbrCurrencyImporter.Domain;
using Microsoft.EntityFrameworkCore;

public class CurrencyContext : DbContext
{
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<CurrencyRate> CurrencyRates { get; set; }

    public CurrencyContext(DbContextOptions<CurrencyContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasMaxLength(10);
            entity.Property(c => c.NumCode).HasMaxLength(5);
            entity.Property(c => c.CharCode).HasMaxLength(5);
            entity.Property(c => c.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<CurrencyRate>(entity =>
        {
            entity.HasKey(cr => cr.Id);
            entity.Property(cr => cr.CurrencyId).HasMaxLength(10);

            entity.HasOne(cr => cr.Currency)
                  .WithMany(c => c.Rates)
                  .HasForeignKey(cr => cr.CurrencyId);
        });
    }
}