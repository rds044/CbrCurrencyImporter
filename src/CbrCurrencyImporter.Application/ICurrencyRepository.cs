using CbrCurrencyImporter.Dto;

namespace CbrCurrencyImporter.Application
{
    public interface ICurrencyRepository
	{
		/// <summary>
		/// ��������� ����� ����� � ���� ������.
		/// </summary>
		Task SaveRatesAsync(List<CurrencyRateDto> rates);
	}
}