using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TradeProject.Lib.Model;

namespace TradeProject.Lib.Service
{
    public class XmlInputReader : IXmlInputReader
    {
        public IEnumerable<Trade> GetTrades(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename) || !File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }
            return GetTrades(File.ReadLines(filename));
        }

        public IEnumerable<Trade> GetTrades(IEnumerable<string> lines)
        {
            var serializer = new XmlSerializer(typeof(Trade));
            return lines.Where(line => line.Contains("<Trade "))
                .Select(line =>
                {
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(line)))
                    {
                        return (Trade) serializer.Deserialize(stream);
                    }
                });
        }
    }
}