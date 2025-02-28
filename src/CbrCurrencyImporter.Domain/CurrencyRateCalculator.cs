using System.Globalization;
using System.Xml;

namespace CbrCurrencyImporter.Domain.Services
{
    public class CurrencyRateCalculator
    {
        public CurrencyRate CreateFromXml(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node), "XML-узел не может быть null.");

            var idAttribute = node.Attributes?["ID"];
            if (idAttribute == null)
                throw new ArgumentException("У XML-узла отсутствует атрибут 'ID'.");

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

            return new CurrencyRate
            {
                Id = idAttribute.Value,
                NumCode = short.Parse(numCodeNode.InnerText),
                CharCode = charCodeNode.InnerText,
                Nominal = int.Parse(nominalNode.InnerText),
                Name = nameNode.InnerText,
                Value = decimal.Parse(valueNode.InnerText, CultureInfo.InvariantCulture),
                VunitRate = decimal.Parse(vunitRateNode.InnerText, CultureInfo.InvariantCulture)
            };
        }
    }
}