namespace CbrCurrencyImporter.DTO
{
    public class CurrencyRatesResponseDTO
    {
        public DateTime Date { get; set; } 
        public string Name { get; set; } 
        public List<CurrencyRateDTO> Rates { get; set; } 
    }
}
