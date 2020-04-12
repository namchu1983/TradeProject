using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using TradeProject.Lib.Service;

namespace TradeProject.Lib.Integration.Tests
{
    public class TradeProcessorTests
    {
        private static string GetDataFolder()
        {
            var dllLocation = Assembly.GetExecutingAssembly().Location;
            var binaryLocation = Path.GetDirectoryName(dllLocation);
            return Path.Combine(binaryLocation, "Data");
        }

        [TestCase("input.xml", "output.csv", "resultExample.csv", "logFile")]
        public void Process_should_write_csv_file(string inputFile, string outputFile, string expectedFile, string logFile)
        {
            var inputFilePath = Path.Combine(GetDataFolder(), inputFile);
            var outputFilePath = Path.Combine(GetDataFolder(), outputFile);
            var expectedFilePath = Path.Combine(GetDataFolder(), expectedFile);
            var logFilePath = Path.Combine(GetDataFolder(), logFile);
            ITradeProcessor tradeProcessor = new TradeProcessor(new XmlInputReader(), new TradeAggregator(),
                new CsvWriter(), new LogConfigurator());
            tradeProcessor.Process(inputFilePath, outputFilePath, logFilePath);
            Assert.IsTrue(File.Exists(outputFilePath));
            CollectionAssert.AreEqual(File.ReadAllLines(expectedFilePath), File.ReadAllLines(outputFilePath));
            var outputLogFile = logFilePath + DateTime.Today.ToString("yyyyMMdd");
            Assert.IsTrue(File.Exists(outputLogFile));
        }
    }
}