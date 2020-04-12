using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Serilog;
using TradeProject.Lib.Model;

namespace TradeProject.Lib.Service
{
    public class XmlInputReader : IXmlInputReader, IDisposable
    {
        private readonly StreamReader _streamReader;

        public XmlInputReader(StreamReader streamReader)
        {
            _streamReader = streamReader;
        }

        public XmlInputReader(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
            {
                Log.Error("The file {filename} is invalid", fileName);
                throw new FileNotFoundException(fileName);
            }
            Log.Information("Create XML stream reader from file : {fileName}", fileName);
            _streamReader = new StreamReader(fileName);
        }

        public IEnumerable<Trade> GetTrades()
        {
            Log.Information("Start to read XML file ");

            var serializer = new XmlSerializer(typeof(Trade));
            using (XmlReader reader = XmlReader.Create(_streamReader))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "Trade")
                            {
                                var el = XNode.ReadFrom(reader) as XElement;
                                if (el != null)
                                {
                                    yield return (Trade)serializer.Deserialize(el.CreateReader());
                                }
                            }
                            break;
                    }
                }
            }
            Log.Information("End to read XML file");
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
        }
    }
}