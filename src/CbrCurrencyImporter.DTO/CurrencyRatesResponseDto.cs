namespace CbrCurrencyImporter.DTO
{
    public class CurrencyRatesResponseDto
    {
        public DateTime Date { get; set; } 
        public string Name { get; set; } 
        public List<CurrencyRateDto> Rates { get; set; } 
    }
}
