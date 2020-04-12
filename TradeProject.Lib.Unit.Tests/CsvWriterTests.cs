using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using TradeProject.Lib.Model;
using TradeProject.Lib.Service;

namespace TradeProject.Lib.Unit.Tests
{
    [TestFixture]
    public class CsvWriterTests
    {
        private static string GetTempFolder()
        {
            var dllLocation = Assembly.GetExecutingAssembly().Location;
            var binaryLocation = Path.GetDirectoryName(dllLocation);
            return Path.Combine(binaryLocation, "Temp");
        }

        [OneTimeSetUp]
        public void Init()
        {
            if (Directory.Exists(GetTempFolder()))
            {
                Directory.Delete(GetTempFolder(), true);
            }
            else
            {
                Directory.CreateDirectory(GetTempFolder());
            }
                
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (Directory.Exists(GetTempFolder()))
            {
                Directory.Delete(GetTempFolder(), true);
            }
        }

        [Test]
        public void WriteResult_should_fail()
        {
            ICsvWriter csvWriter = new CsvWriter();
            Assert.Throws<ArgumentException>(() => csvWriter.WriteResult(string.Empty, Enumerable.Empty<CsvModel>()));
        }

        [TestCaseSource(nameof(CsvWriterTestScenarios))]
        public void WriteResult_should_write_csv_file(string fileName, IEnumerable<CsvModel> models,
            IEnumerable<string> expected)
        {
            ICsvWriter csvWriter = new CsvWriter();
            csvWriter.WriteResult(fileName, models);
            CollectionAssert.AreEqual(expected, File.ReadAllLines(fileName));
        }

        public static object[] CsvWriterTestScenarios =
        {
            ZeroAggregatedTradeScenario(),
            OneAggregatedTradeScenario(),
            TwoAggregatedTradesScenario()
        };

        private static object[] ZeroAggregatedTradeScenario()
        {
            return new object[]
            {
                Path.Combine(GetTempFolder(), $"{nameof(ZeroAggregatedTradeScenario)}.csv"),
                new CsvModel[] { },
                new[]
                {
                    "CorrelationID,NumberOfTrades,State"
                }
            };
        }

        private static object[] OneAggregatedTradeScenario()
        {
            return new object[]
            {
                Path.Combine(GetTempFolder(), $"{nameof(OneAggregatedTradeScenario)}.csv"),
                new[]
                {
                    new CsvModel
                    {
                        CorrelationID = "1",
                        NumberOfTrades = 1,
                        State = "Accepted"
                    }
                },
                new[]
                {
                    "CorrelationID,NumberOfTrades,State",
                    "1,1,Accepted"
                }
            };
        }

        private static object[] TwoAggregatedTradesScenario()
        {
            return new object[]
            {
                Path.Combine(GetTempFolder(), $"{nameof(TwoAggregatedTradesScenario)}.csv"),
                new[]
                {
                    new CsvModel
                    {
                        CorrelationID = "1",
                        NumberOfTrades = 1,
                        State = "Accepted"
                    },
                    new CsvModel
                    {
                        CorrelationID = "2",
                        NumberOfTrades = 1,
                        State = "Rejected"
                    }
                },
                new[]
                {
                    "CorrelationID,NumberOfTrades,State",
                    "1,1,Accepted",
                    "2,1,Rejected"
                }
            };
        }
    }
}