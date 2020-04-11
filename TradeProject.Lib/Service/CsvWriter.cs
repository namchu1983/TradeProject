using System.Collections.Generic;
using LINQtoCSV;
using TradeProject.Lib.Model;

namespace TradeProject.Lib.Service
{
    public class CsvWriter : ICsvWriter
    {
        public void WriteResult(string fileName, IEnumerable<CsvModel> models)
        {
            var outputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',', FirstLineHasColumnNames = true, EnforceCsvColumnAttribute = true
            };
            new CsvContext().Write(models, fileName, outputFileDescription);
        }
    }
}