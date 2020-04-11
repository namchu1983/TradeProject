namespace TradeProject.Lib.Service
{
    public interface ITradeProcessor
    {
        void Process(string inputFile, string outputFile);
    }
}