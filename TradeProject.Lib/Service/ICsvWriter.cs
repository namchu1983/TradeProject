using System.Collections.Generic;
using TradeProject.Lib.Model;

namespace TradeProject.Lib.Service
{
    public interface ICsvWriter
    {
        void WriteResult(string fileName, IEnumerable<CsvModel> models);
    }
}