using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using TradeProject.Lib.Model;
using TradeProject.Lib.Service;

namespace TradeProject.Lib.Unit.Tests
{
    public class XmlInputReaderTests
    {
        public static object[] XmlInputReaderTestScenarios =
        {
            ZeroTradeScenario(),
            OneTradeScenario(),
            TwoTradesScenario()
        };

        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        [TestCase("Toto")]
        public void GetTrades_should_throw_FileNotFoundException_when_fileName_is_not_valid(string fileName)
        {

            Assert.Throws<FileNotFoundException>(() =>
            {
                IXmlInputReader xmlInputReader = new XmlInputReader();
                xmlInputReader.GetTrades(fileName);
            });
        }

        [TestCaseSource(nameof(XmlInputReaderTestScenarios))]
        public void GetTrades_should_return(IEnumerable<string> lines, IEnumerable<Trade> expected)
        {
            IXmlInputReader xmlInputReader = new XmlInputReader();
            var actual = xmlInputReader.GetTrades(lines);
            CollectionAssert.AreEqual(expected.ToList(), actual.ToList());
        }

        private static object[] ZeroTradeScenario()
        {
            return new object[] {new string[] { }, new Trade[] { }};
        }

        private static object[] OneTradeScenario()
        {
            return new object[]
            {
                new[]
                {
                    "<Trade CorrelationId=\"Toto\" NumberOfTrades=\"1\" Limit=\"1000\" TradeID=\"Tata\">100</Trade>"
                },
                new[]
                {
                    new Trade
                    {
                        CorrelationId = "Toto",
                        NumberOfTrades = 1,
                        Limit = 1000,
                        TradeID = "Tata",
                        Value = 100
                    }
                }
            };
        }

        private static object[] TwoTradesScenario()
        {
            return new object[]
            {
                new[]
                {
                    "<Trade CorrelationId=\"Toto\" NumberOfTrades=\"1\" Limit=\"1000\" TradeID=\"Tata\">100</Trade>",
                    "<Trade CorrelationId=\"Titi\" NumberOfTrades=\"1\" Limit=\"1000\" TradeID=\"Tutu\">100</Trade>"
                },
                new[]
                {
                    new Trade
                    {
                        CorrelationId = "Toto",
                        NumberOfTrades = 1,
                        Limit = 1000,
                        TradeID = "Tata",
                        Value = 100
                    },
                    new Trade
                    {
                        CorrelationId = "Titi",
                        NumberOfTrades = 1,
                        Limit = 1000,
                        TradeID = "Tutu",
                        Value = 100
                    }
                }
            };
        }
    }
}