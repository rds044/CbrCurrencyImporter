using CbrCurrencyImporter.Domain;
using CbrCurrencyImporter.Dto;
using CbrCurrencyImporter.Application;
namespace CbrCurrencyImporter.Infrastructure.Data
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly CurrencyContext _context;

        public CurrencyRepository(CurrencyContext context)
        {
            _context = context;
        }

        public async Task SaveRatesAsync(List<CurrencyRateDto> rates)
        {
            foreach (var rateDto in rates)
            {
                // Проверяем, существует ли валюта в базе данных
                var currency = await _context.Currencies.FindAsync(rateDto.Id);
                if (currency == null)
                {
                    currency = new Currency
                    {
                        Id = rateDto.Id,
                        NumCode = rateDto.NumCode,
                        CharCode = rateDto.CharCode,
                        Nominal = rateDto.Nominal,
                        Name = rateDto.Name
                    };
                    _context.Currencies.Add(currency);
                }

                // Создаем объект CurrencyRate с использованием конструктора
                var currencyRate = new CurrencyRate(
                    id: rateDto.Id,
                    numCode: rateDto.NumCode,
                    charCode: rateDto.CharCode,
                    nominal: rateDto.Nominal,
                    name: rateDto.Name,
                    value: rateDto.Value,
                    vunitRate: rateDto.VunitRate
                );

                // Добавляем курс валюты
                _context.CurrencyRates.Add(currencyRate);
            }

            await _context.SaveChangesAsync();
        }
    }
}