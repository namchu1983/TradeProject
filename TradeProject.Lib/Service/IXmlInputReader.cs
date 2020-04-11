using System.Collections.Generic;
using TradeProject.Lib.Model;

namespace TradeProject.Lib.Service
{
    public interface IXmlInputReader
    {
        IEnumerable<Trade> GetTrades(string filename);
        IEnumerable<Trade> GetTrades(IEnumerable<string> lines);
    }
}