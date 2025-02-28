using CbrCurrencyImporter.Dto;
using CbrCurrencyImporter.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace CbrCurrencyImporter.Application.Internal
{
    public class CbrCurrencyParser : ICbrCurrencyParser
    {
        private readonly HttpClient _httpClient;

        // Конструктор для инициализации HttpClient
        public CbrCurrencyParser(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Метод для парсинга курсов валют за указанную дату
        public async Task<List<CurrencyRateDto>> ParseCurrencyRatesAsync(DateTime date)
        {
            // Формируем URL для запроса данных с сайта ЦБ РФ
            var url = $"https://www.cbr.ru/scripts/XML_daily.asp?date_req={date:dd/MM/yyyy}";

            // Выполняем HTTP-запрос и получаем XML-ответ
            var response = await _httpClient.GetStringAsync(url);

            // Загружаем XML-документ
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);

            // Парсим атрибуты корневого элемента
            var dateAttribute = xmlDoc.DocumentElement?.Attributes?["Date"];
            var nameAttribute = xmlDoc.DocumentElement?.Attributes?["name"];

            // Проверяем, что атрибуты существуют
            if (dateAttribute == null || nameAttribute == null)
            {
                throw new ArgumentException("XML-документ содержит неполные данные.");
            }

            // Парсим дату из атрибута
            var responseDate = DateTime.ParseExact(dateAttribute.Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);

            // Создаем список для хранения курсов валют
            var rates = new List<CurrencyRateDto>();

            // Парсим каждый элемент <Valute> в XML
            foreach (XmlNode node in xmlDoc.SelectNodes("//Valute"))
            {
                // Создаем объект CurrencyRate из XML-узла
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

            // Возвращаем список DTO
            return rates;
        }
    }
}