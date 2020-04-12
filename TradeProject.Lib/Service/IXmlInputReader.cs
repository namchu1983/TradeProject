using System.Collections.Generic;
using System.IO;
using TradeProject.Lib.Model;

namespace TradeProject.Lib.Service
{
    public interface IXmlInputReader
    {
        IEnumerable<Trade> GetTrades();
    }
}