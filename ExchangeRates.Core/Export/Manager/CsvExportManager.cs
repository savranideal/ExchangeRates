using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Export
{
    public class CsvExportManager : ExportManager
    {

        public Task<CsvExportResult> ExportAsync<TInput>(TInput input) where TInput : class
        {

            var t = new CsvExportResult();
            try
            { 
                t.Data = CreateCSV(input);
                t.Succeeded = true; 
            }
            catch (Exception e)
            {
                t.ErrorMessage = e.Message;
                t.Succeeded = false;
            }

            return Task.FromResult(t);

        }
        public Task<CsvExportResult> ExportAsync<TInput>(IEnumerable<TInput> input) where TInput : class
        {

            var t = new CsvExportResult();
            try
            {

                t.Data = CreateCSV(input); 
                t.Succeeded = true;
            }
            catch (Exception e)
            {
                t.ErrorMessage = e.Message;
                t.Succeeded = false;
            }

            return Task.FromResult(t);

        }

        private string CreateCSV<T>(IEnumerable<T> objects)
        {
            if (objects == null || objects.Count() == 0) return null;
            Type t = objects.First().GetType();

            StringBuilder sb = new StringBuilder();

            PropertyInfo[] props = t.GetProperties();
            sb.AppendLine(string.Join(";", props.Select(d => d.Name).ToArray()));

            foreach (T item in objects)
            {
                sb.AppendLine(string.Join(";", props.Select(d => item.GetType()
                                                                .GetProperty(d.Name)
                                                                .GetValue(item, null)
                                                                ?.ToString())?.ToArray()));

            }
            return sb.ToString();
        }

        private string CreateCSV<T>(T input)
        {
            if (input == null) return null;
            Type t = input.GetType();

            StringBuilder sb = new StringBuilder();

            PropertyInfo[] props = t.GetProperties();
            sb.AppendLine(string.Join(";", props.Select(d => d.Name).ToArray()));


            sb.AppendLine(string.Join(";", props.Select(d => input.GetType()
                                                            .GetProperty(d.Name)
                                                            .GetValue(input, null)
                                                            ?.ToString())?.ToArray()));

            return sb.ToString();
        }

    }
}
