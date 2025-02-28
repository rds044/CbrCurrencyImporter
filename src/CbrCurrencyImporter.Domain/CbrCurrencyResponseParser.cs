using System;
using System.Collections.Generic;

namespace CbrCurrencyImporter.Domain
{
    /// <summary>
    /// Представляет ответ от API ЦБР с курсами валют.
    /// </summary>
    public class CbrCurrencyResponseParser
    {
        /// <summary>
        /// Дата курса.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Название документа.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Список курсов валют.
        /// </summary>
        public List<CurrencyRate> Rates { get; }

        /// <summary>
        /// Создает новый объект CurrencyRatesResponse.
        /// </summary>
        public CbrCurrencyResponseParser(DateTime date, string name, List<CurrencyRate> rates)
        {
            Date = date;
            Name = name;
            Rates = rates;
        }
    }
}