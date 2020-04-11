namespace TradeProject.Lib.Service
{
    public class TradeProcessor : ITradeProcessor
    {
        private readonly ICsvWriter _csvWriter;
        private readonly ITradeAggregator _tradeAggregator;
        private readonly IXmlInputReader _xmlInputReader;

        public TradeProcessor(IXmlInputReader xmlInputReader, ITradeAggregator tradeAggregator, ICsvWriter csvWriter)
        {
            _xmlInputReader = xmlInputReader;
            _tradeAggregator = tradeAggregator;
            _csvWriter = csvWriter;
        }

        public void Process(string inputFile, string outputFile)
        {
            var trades = _xmlInputReader.GetTrades(inputFile);
            var csvModels = _tradeAggregator.Aggregate(trades);
            _csvWriter.WriteResult(outputFile, csvModels);
        }
    }
}