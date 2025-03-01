﻿namespace CbrCurrencyImporter.Domain
{
    public class Currency
    {
        public string Id { get; set; }
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }

        public List<CurrencyRate> Rates { get; set; } = new List<CurrencyRate>();
    }
}
