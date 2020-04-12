using System;
using System.Collections.Generic;
using LINQtoCSV;
using Serilog;
using TradeProject.Lib.Model;

namespace TradeProject.Lib.Service
{
    public class CsvWriter : ICsvWriter
    {
        public void WriteResult(string fileName, IEnumerable<CsvModel> models)
        {
            Log.Information("Start to write output result {fileName}!", fileName);
            try
            {
                var outputFileDescription = new CsvFileDescription
                {
                    SeparatorChar = ',',
                    FirstLineHasColumnNames = true,
                    EnforceCsvColumnAttribute = true
                };
                new CsvContext().Write(models, fileName, outputFileDescription);
                Log.Information("Success to write output result!");
            }
            catch (Exception)
            {
                Log.Information("Fail to write output result!");
                throw;
            }

        }
    }
}