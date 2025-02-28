using CbrCurrencyImporter.Dto;

namespace CbrCurrencyImporter.Application
{
    public interface ICbrCurrencyParser
    {
        /// <summary>
        /// Получает курсы валют за указанную дату.
        /// </summary>
        /// <param name="date">Дата, за которую нужно получить курсы.</param>
        /// <returns>Ответ от API ЦБР с курсами валют.</returns>
        Task<List<CurrencyRateDto>> ParseCurrencyRatesAsync(DateTime date);
    }
}