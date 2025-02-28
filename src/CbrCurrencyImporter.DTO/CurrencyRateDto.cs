namespace CbrCurrencyImporter.DTO
{
    /// <summary>
    /// DTO модель курсов валют, ЦБ всегда выводит все курсы валют, поэтому nullable не использую
    /// </summary>
    public class CurrencyRateDto
    {
        /// <summary>
        /// Id валюты
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ISO Цифр. код валюты
        /// </summary>
        public short NumCode { get; set; }

        /// <summary>
        /// ISO Букв. код валюты
        /// </summary>
        public string CharCode { get; set; }
        
        /// <summary>
        /// Номинал. ед
        /// </summary>
        public int Nominal { get; set; }

        /// <summary>
        /// Название валюты
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Курс валюты
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Курс за одну единицу валюты
        /// </summary>
        public decimal VunitRate { get; set; }

    }
}
