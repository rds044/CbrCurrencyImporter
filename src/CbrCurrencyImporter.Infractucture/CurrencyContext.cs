using CbrCurrencyImporter.Domain;
using Microsoft.EntityFrameworkCore;

namespace CbrCurrencyImporter.Infrastructure.Data
{
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
            // Настройка таблицы Currencies
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(c => c.Id); // Первичный ключ
                entity.Property(c => c.Id).HasMaxLength(10); // Ограничение длины Id
                entity.Property(c => c.CharCode).HasMaxLength(5); // Ограничение длины CharCode
                entity.Property(c => c.Name).HasMaxLength(100); // Ограничение длины Name
            });

            // Настройка таблицы CurrencyRates
            modelBuilder.Entity<CurrencyRate>(entity =>
            {
                entity.HasKey(cr => new { cr.Id, cr.Date }); // Составной первичный ключ
                entity.Property(cr => cr.CurrencyId).HasMaxLength(10); // Ограничение длины CurrencyId

                // Настройка связи с Currency
                entity.HasOne(cr => cr.Currency)
                      .WithMany(c => c.Rates)
                      .HasForeignKey(cr => cr.CurrencyId);
            });
        }
    }
}