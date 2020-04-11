using System.Collections.Generic;
using NUnit.Framework;
using TradeProject.Lib.Model;
using TradeProject.Lib.Service;

namespace TradeProject.Lib.Unit.Tests
{
    public class TradeAggregatorTests
    {
        public static object[] TradeAggregatorTestScenarios =
        {
            ZeroTradeScenario(),
            OneAcceptedTradeScenario(),
            OneRejectedTradeScenario(),
            OnePendingTradeScenario(),
            TwoTradesWithSortingScenario(),
            TwoTradesOfSameCorrelationIdScenario()
        };

        [TestCaseSource(nameof(TradeAggregatorTestScenarios))]
        public void Aggregate_should_return(IEnumerable<Trade> trades, IEnumerable<CsvModel> expected)
        {
            ITradeAggregator tradeAggregator = new TradeAggregator();
            CollectionAssert.AreEqual(expected, tradeAggregator.Aggregate(trades));
        }

        private static object[] ZeroTradeScenario()
        {
            return new object[] {new Trade[] { }, new CsvModel[] { }};
        }

        private static object[] OneAcceptedTradeScenario()
        {
            return new object[]
            {
                new[]
                {
                    new Trade
                    {
                        CorrelationId = "1",
                        NumberOfTrades = 1,
                        Limit = 1000,
                        TradeID = "Tata",
                        Value = 100
                    }
                },
                new[]
                {
                    new CsvModel
                    {
                        CorrelationID = "1",
                        NumberOfTrades = 1,
                        State = "Accepted"
                    }
                }
            };
        }

        private static object[] OneRejectedTradeScenario()
        {
            return new object[]
            {
                new[]
                {
                    new Trade
                    {
                        CorrelationId = "1",
                        NumberOfTrades = 1,
                        Limit = 1000,
                        TradeID = "Tata",
                        Value = 1000
                    }
                },
                new[]
                {
                    new CsvModel
                    {
                        CorrelationID = "1",
                        NumberOfTrades = 1,
                        State = "Rejected"
                    }
                }
            };
        }

        private static object[] OnePendingTradeScenario()
        {
            return new object[]
            {
                new[]
                {
                    new Trade
                    {
                        CorrelationId = "1",
                        NumberOfTrades = 2,
                        Limit = 1000,
                        TradeID = "Tata",
                        Value = 500
                    }
                },
                new[]
                {
                    new CsvModel
                    {
                        CorrelationID = "1",
                        NumberOfTrades = 2,
                        State = "Pending"
                    }
                }
            };
        }

        private static object[] TwoTradesWithSortingScenario()
        {
            return new object[]
            {
                new[]
                {
                    new Trade
                    {
                        CorrelationId = "2",
                        NumberOfTrades = 1,
                        Limit = 1000,
                        TradeID = "Tata",
                        Value = 100
                    },
                    new Trade
                    {
                        CorrelationId = "1",
                        NumberOfTrades = 1,
                        Limit = 1000,
                        TradeID = "Tutu",
                        Value = 1000
                    }
                },
                new[]
                {
                    new CsvModel
                    {
                        CorrelationID = "1",
                        NumberOfTrades = 1,
                        State = "Rejected"
                    },
                    new CsvModel
                    {
                        CorrelationID = "2",
                        NumberOfTrades = 1,
                        State = "Accepted"
                    }
                }
            };
        }

        private static object[] TwoTradesOfSameCorrelationIdScenario()
        {
            return new object[]
            {
                new[]
                {
                    new Trade
                    {
                        CorrelationId = "1",
                        NumberOfTrades = 2,
                        Limit = 1000,
                        TradeID = "Tata",
                        Value = 700
                    },
                    new Trade
                    {
                        CorrelationId = "1",
                        NumberOfTrades = 2,
                        Limit = 1000,
                        TradeID = "Tutu",
                        Value = 700
                    }
                },
                new[]
                {
                    new CsvModel
                    {
                        CorrelationID = "1",
                        NumberOfTrades = 2,
                        State = "Rejected"
                    }
                }
            };
        }
    }
}