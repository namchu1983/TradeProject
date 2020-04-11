using System.Collections.Generic;
using TradeProject.Lib.Model;

namespace TradeProject.Lib.Service
{
    public interface ITradeAggregator
    {
        IEnumerable<CsvModel> Aggregate(IEnumerable<Trade> trades);
    }
}