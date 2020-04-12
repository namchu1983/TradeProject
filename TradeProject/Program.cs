using TradeProject.Lib.Service;

namespace TradeProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var inputFilePath = @""; // absolute path
            var outputFilePath = @""; // absolute path
            var logFilePath = @""; // absolute path
            ITradeProcessor tradeProcessor = new TradeProcessor();
            tradeProcessor.Process(inputFilePath, outputFilePath, logFilePath);
        }
    }
}