using CbrCurrencyImporter.Dto;
using CbrCurrencyImporter.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Text;

namespace CbrCurrencyImporter.Application.Internal
{
    public class CbrCurrencyParser : ICbrCurrencyParser
    {
        private readonly HttpClient _httpClient;

        public CbrCurrencyParser(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CurrencyRateDto>> ParseCurrencyRatesAsync(DateTime date)
        {
            var url = $"https://www.cbr.ru/scripts/XML_daily.asp?date_req={date:dd/MM/yyyy}";

            var response = await _httpClient.GetByteArrayAsync(url);

            var encoding = Encoding.GetEncoding("windows-1251");
            var responseString = encoding.GetString(response);

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseString);

            var dateAttribute = xmlDoc.DocumentElement?.Attributes?["Date"];
            var nameAttribute = xmlDoc.DocumentElement?.Attributes?["name"];

            if (dateAttribute == null || nameAttribute == null)
            {
                throw new ArgumentException("XML-документ содержит неполные данные.");
            }

            var responseDate = DateTime.ParseExact(dateAttribute.Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var responseName = nameAttribute.Value;

            var rates = new List<CurrencyRateDto>();
            foreach (XmlNode node in xmlDoc.SelectNodes("//Valute"))
            {
                // Парсим XML-узел и создаем объект CurrencyRate
                var rate = CurrencyRate.FromXml(node, responseDate);

                // Преобразуем CurrencyRate в CurrencyRateDto
                rates.Add(new CurrencyRateDto
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

            return rates;
        }
    }
}