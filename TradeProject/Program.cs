using System;
using TradeProject.Lib.Service;

namespace TradeProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var inputFilePath = ""; // absolute path
            var outputFilePath = ""; // absolute path
            ITradeProcessor tradeProcessor = new TradeProcessor(new XmlInputReader(), new TradeAggregator(), new CsvWriter());
            tradeProcessor.Process(inputFilePath, outputFilePath);
            Console.WriteLine("Done");
        }
    }
}