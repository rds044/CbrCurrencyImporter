using CbrCurrencyImporter.Application;
using Microsoft.AspNetCore.Mvc;

namespace CbrCurrencyImporter.Api.Controllers
{
    [ApiController]
    [Route("api/currency")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICbrCurrencyParser _cbrCurrencyParser;

        public CurrencyController(ICbrCurrencyParser cbrCurrencyParser)
        {
            _cbrCurrencyParser = cbrCurrencyParser;
        }

        /// <summary>
        /// Получает курсы валют за указанную дату.
        /// </summary>
        /// <param name="date">Дата, за которую нужно получить курсы. Если не указана, используется текущая дата.</param>
        /// <returns>Ответ с курсами валют в формате JSON.</returns>
        [HttpGet("rates")]
        public async Task<IActionResult> GetRates([FromQuery] DateTime? date = null)
        {
            var targetDate = date ?? DateTime.Today;

            var response = await _cbrCurrencyParser.ParseCurrencyRatesAsync(targetDate);

            return Ok(response);
        }
    }
}