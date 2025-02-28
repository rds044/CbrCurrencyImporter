using System.Globalization;
using System.Xml;

namespace CbrCurrencyImporter.Domain
{
    /// <summary>
    /// Представляет курс одной валюты.
    /// </summary>
    public class CurrencyRate
    {
        /// <summary>
        /// Уникальный идентификатор валюты.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Цифровой код валюты по ISO.
        /// </summary>
        public short NumCode { get; }

        /// <summary>
        /// Буквенный код валюты по ISO.
        /// </summary>
        public string CharCode { get; }

        /// <summary>
        /// Номинал валюты (количество единиц валюты, к которому применяется курс).
        /// </summary>
        public int Nominal { get; }

        /// <summary>
        /// Название валюты.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Курс валюты.
        /// </summary>
        public decimal Value { get; }

        /// <summary>
        /// Курс за одну единицу валюты.
        /// </summary>
        public decimal VunitRate { get; }

        /// <summary>
        /// Создает новый объект CurrencyRate.
        /// </summary>
        public CurrencyRate(string id, short numCode, string charCode, int nominal, string name, decimal value, decimal vunitRate)
        {
            Id = id;
            NumCode = numCode;
            CharCode = charCode;
            Nominal = nominal;
            Name = name;
            Value = value;
            VunitRate = vunitRate;
        }

        /// <summary>
        /// Создает объект CurrencyRate из XML-узла.
        /// </summary>
        /// <param name="node">XML-узел, содержащий данные о валюте.</param>
        /// <returns>Объект CurrencyRate.</returns>
        public static CurrencyRate FromXml(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node), "XML-узел не может быть null.");

            // Проверяем атрибут "ID"
            var idAttribute = node.Attributes?["ID"];
            if (idAttribute == null)
                throw new ArgumentException("У XML-узла отсутствует атрибут 'ID'.");

            // Проверяем и извлекаем значения из дочерних узлов
            var numCodeNode = node.SelectSingleNode("NumCode");
            var charCodeNode = node.SelectSingleNode("CharCode");
            var nominalNode = node.SelectSingleNode("Nominal");
            var nameNode = node.SelectSingleNode("Name");
            var valueNode = node.SelectSingleNode("Value");
            var vunitRateNode = node.SelectSingleNode("VunitRate");

            if (numCodeNode == null || charCodeNode == null || nominalNode == null ||
                nameNode == null || valueNode == null || vunitRateNode == null)
            {
                throw new ArgumentException("XML-узел содержит неполные данные.");
            }

            // Парсим значения
            return new CurrencyRate(
                id: idAttribute.Value,
                numCode: short.Parse(numCodeNode.InnerText),
                charCode: charCodeNode.InnerText,
                nominal: int.Parse(nominalNode.InnerText),
                name: nameNode.InnerText,
                value: decimal.Parse(valueNode.InnerText, CultureInfo.InvariantCulture),
                vunitRate: decimal.Parse(vunitRateNode.InnerText, CultureInfo.InvariantCulture)
            );
        }
    }
}