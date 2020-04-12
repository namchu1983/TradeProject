using Serilog;

namespace TradeProject.Lib.Service
{
    public class LogConfigurator : ILogConfigurator
    {
        public void Configure(string logFile)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(logFile, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}