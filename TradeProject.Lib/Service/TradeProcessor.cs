using System;
using Serilog;

namespace TradeProject.Lib.Service
{
    public class TradeProcessor : ITradeProcessor
    {

        public void Process(string inputFile, string outputFile, string logFile)
        {
            try
            {
                new LogConfigurator().Configure(logFile);
                Log.Information("Start to process");
                using (var xmlInputReader  = new XmlInputReader(inputFile))
                {
                    var trades = xmlInputReader.GetTrades();
                    var csvModels = new TradeAggregator().Aggregate(trades);
                    new CsvWriter().WriteResult(outputFile, csvModels);
                }
                Log.Information("End to process : success");
            }
            catch (Exception e)
            {
                Log.Error(e, "Some error during process. Check the log file!");
                Log.Information("End to process : error");
            }
        }
    }
}