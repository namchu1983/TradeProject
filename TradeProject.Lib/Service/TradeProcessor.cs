using System;
using Serilog;

namespace TradeProject.Lib.Service
{
    public class TradeProcessor : ITradeProcessor
    {
        private readonly ICsvWriter _csvWriter;
        private readonly ITradeAggregator _tradeAggregator;
        private readonly IXmlInputReader _xmlInputReader;
        private readonly ILogConfigurator _logConfigurator;

        public TradeProcessor(IXmlInputReader xmlInputReader, ITradeAggregator tradeAggregator, ICsvWriter csvWriter, ILogConfigurator logConfigurator)
        {
            _xmlInputReader = xmlInputReader;
            _tradeAggregator = tradeAggregator;
            _csvWriter = csvWriter;
            _logConfigurator = logConfigurator;
        }

        public void Process(string inputFile, string outputFile, string logFile)
        {
            try
            {
                _logConfigurator.Configure(logFile);
                Log.Information("Start to process");
                var trades = _xmlInputReader.GetTrades(inputFile);
                var csvModels = _tradeAggregator.Aggregate(trades);
                _csvWriter.WriteResult(outputFile, csvModels);
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