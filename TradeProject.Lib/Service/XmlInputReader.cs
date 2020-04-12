using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Serilog;
using TradeProject.Lib.Model;

namespace TradeProject.Lib.Service
{
    public class XmlInputReader : IXmlInputReader
    {
        public IEnumerable<Trade> GetTrades(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename) || !File.Exists(filename))
            {
                Log.Error("The file {filename} is invalid", filename);
                throw new FileNotFoundException(filename);
            }
            Log.Information("Start to read XML file : {filename}", filename);
            var trades = GetTrades(File.ReadLines(filename));
            Log.Information("End to read XML file : {filename}", filename);
            return trades;
        }

        public IEnumerable<Trade> GetTrades(IEnumerable<string> lines)
        {
            var serializer = new XmlSerializer(typeof(Trade));
            return lines.Where(line => line.Contains("<Trade "))
                .Select(line =>
                {
                    try
                    {
                        using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(line)))
                        {
                            return (Trade)serializer.Deserialize(stream);
                        }
                    }
                    catch (Exception)
                    {
                        Log.Error("Cannot parse this line to Trade object:\n{line} ", line);
                        throw;
                    }
                });
        }
    }
}