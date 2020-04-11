using System.Collections.Generic;
using System.Linq;
using TradeProject.Lib.Model;

namespace TradeProject.Lib.Service
{
    public class TradeAggregator : ITradeAggregator
    {
        public IEnumerable<CsvModel> Aggregate(IEnumerable<Trade> trades)
        {
            return ProcessOneByOne(trades);
        }

        private IEnumerable<CsvModel> ProcessOneByOne(IEnumerable<Trade> trades)
        {
            var csvModels = new Dictionary<string, CsvModel>();
            foreach (var trade in trades)
            {
                var correlationId = trade.CorrelationId;
                var csvModel = csvModels.ContainsKey(correlationId) ? csvModels[correlationId] : new CsvModel
                {
                    CorrelationID = correlationId,
                    NumberOfTrades = trade.NumberOfTrades,
                    Limit = trade.Limit
                };
                csvModel.Count += 1;
                csvModel.Value += trade.Value;
                csvModels[correlationId] = csvModel;
            }

            foreach (var csvModel in csvModels.Values)
            {
                csvModel.State = csvModel.Count != csvModel.NumberOfTrades ? "Pending" :
                    csvModel.Value < csvModel.Limit ? "Accepted" : "Rejected";
            }
            
            return csvModels.OrderBy(pair => pair.Key).Select(pair => pair.Value);
        }

        //private IEnumerable<CsvModel> ProcessUsingLinq(IEnumerable<Trade> trades)
        //{
        //    return trades.GroupBy(trade => trade.CorrelationId).Select(group =>
        //        {
        //            var tradesTrades = group.ToList();
        //            var first = tradesTrades.First();
        //            var numberOfTrades = first.NumberOfTrades;
        //            var limit = first.Limit;
        //            var sum = tradesTrades.Sum(trade => trade.Value);
        //            var state = tradesTrades.Count != numberOfTrades ? "Pending" :
        //                sum < limit ? "Accepted" : "Rejected";
        //            return new CsvModel
        //            {
        //                CorrelationID = first.CorrelationId,
        //                NumberOfTrades = numberOfTrades,
        //                State = state
        //            };
        //        }
        //    ).OrderBy(model => model.CorrelationID);
        //}
    }
}