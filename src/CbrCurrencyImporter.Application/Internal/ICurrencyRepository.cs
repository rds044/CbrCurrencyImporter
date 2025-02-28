using System.Collections.Generic;
using System.Threading.Tasks;

namespace CbrCurrencyImporter.Domain
{
	public interface ICurrencyRepository
	{
		/// <summary>
		/// ��������� ����� ����� � ���� ������.
		/// </summary>
		Task SaveRatesAsync(List<CurrencyRate> rates);
	}
}