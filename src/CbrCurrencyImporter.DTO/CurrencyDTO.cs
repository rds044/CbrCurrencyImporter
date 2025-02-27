using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbrCurrencyImporter.Api
{
    /// <summary>
    /// DTO модель курсов валют, ЦБ всегда выводит все курсы валют, поэтому nullable не использую
    /// </summary>
    public class CurrencyDTO
    {
        /// <summary>
        /// Код курса валют
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Сокращенное имя валюты
        /// </summary>
        public string CurrencyName { get; set; }
        /// <summary>
        /// Стоимость в рублях 1 шт иностранной валюты  
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Количество шт иностранной валюты приравненно к Value 
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// Количество шт иностранной валюты приравненно к Value 
        /// </summary> 
        public int Nominal { get; set; }
        /// <summary>
        /// Время и дата сбора данных
        /// </summary>
        public DateTime Date { get; set; }
    }
}
