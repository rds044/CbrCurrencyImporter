using System;
using System.Globalization;
using System.Xml;

namespace CbrCurrencyImporter.Domain
{
    public class CurrencyRate
    {
        // Свойства для хранения данных о курсе валюты
        public string Id { get; }
        public short NumCode { get; }
        public string CharCode { get; }
        public int Nominal { get; }
        public string Name { get; }
        public decimal Value { get; }
        public decimal VunitRate { get; }

        public DateTime Date { get; set; }

        // Внешний ключ для связи с таблицей Currencies
        public string CurrencyId { get; set; }

        // Навигационное свойство для связи с Currency
        public Currency Currency { get; set; }

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

        public static CurrencyRate FromXml(XmlNode node, DateTime date)
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

            var nominal = int.Parse(nominalNode.InnerText);
            var value = decimal.Parse(valueNode.InnerText, CultureInfo.InvariantCulture);
            var vunitRate = decimal.Parse(vunitRateNode.InnerText, CultureInfo.InvariantCulture);

            // Корректируем значение курса
            var correctedValue = value / nominal;
            var correctedVunitRate = vunitRate / nominal;

            return new CurrencyRate(
                id: idAttribute.Value,
                numCode: short.Parse(numCodeNode.InnerText),
                charCode: charCodeNode.InnerText,
                nominal: nominal,
                name: nameNode.InnerText,
                value: correctedValue,
                vunitRate: correctedVunitRate
            )
            {
                Date = date // Устанавливаем дату
            };
        }
    }
    }

