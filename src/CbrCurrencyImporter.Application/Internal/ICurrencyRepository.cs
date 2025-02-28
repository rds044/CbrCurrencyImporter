using System.Collections.Generic;
using System.Threading.Tasks;

namespace CbrCurrencyImporter.Domain
{
	public interface ICurrencyRepository
	{
		/// <summary>
		/// Сохраняет курсы валют в базу данных.
		/// </summary>
		Task SaveRatesAsync(List<CurrencyRate> rates);
	}
}