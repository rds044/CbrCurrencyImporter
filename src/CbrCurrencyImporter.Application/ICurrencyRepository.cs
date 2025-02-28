using CbrCurrencyImporter.Dto;

namespace CbrCurrencyImporter.Application
{
    public interface ICurrencyRepository
	{
		/// <summary>
		/// Сохраняет курсы валют в базу данных.
		/// </summary>
		Task SaveRatesAsync(List<CurrencyRateDto> rates);
	}
}