using CbrCurrencyImporter.Domain;
using CbrCurrencyImporter.DTO;
using System.Xml;

namespace CbrCurrencyImporter.Application.Internal
{
    public class CbrCurrencyParser : ICbrCurrencyParser
    {
        private readonly HttpClient _httpClient;

        public CbrCurrencyParser(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc />
        public async Task<List<CurrencyRateDto>> ParseCurrencyRatesAsync(DateTime date)
        {
            // Формируем URL для запроса
            var url = $"https://www.cbr.ru/scripts/XML_daily.asp?date_req={date:dd/MM/yyyy}";

            // Выполняем HTTP-запрос
            var response = await _httpClient.GetStringAsync(url);

            // Парсим XML
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);

            // Извлекаем данные с помощью доменной модели
            var rates = new List<CurrencyRate>();
            foreach (XmlNode node in xmlDoc.SelectNodes("//Valute"))
            {
                rates.Add(CurrencyRate.FromXml(node));
            }

            // Преобразуем доменную модель в DTO
            var rateDtos = ConvertToDto(rates);

            return rateDtos;
        }

        /// <summary>
        /// Преобразует список доменных моделей CurrencyRate в список DTO.
        /// </summary>
        /// <param name="rates">Список доменных моделей.</param>
        /// <returns>Список DTO.</returns>
        private List<CurrencyRateDto> ConvertToDto(List<CurrencyRate> rates)
        {
            var rateDtos = new List<CurrencyRateDto>();

            foreach (var rate in rates)
            {
                rateDtos.Add(new CurrencyRateDto
                {
                    Id = rate.Id,
                    NumCode = rate.NumCode,
                    CharCode = rate.CharCode,
                    Nominal = rate.Nominal,
                    Name = rate.Name,
                    Value = rate.Value,
                    VunitRate = rate.VunitRate
                });
            }

            return rateDtos;
        }
    }
}