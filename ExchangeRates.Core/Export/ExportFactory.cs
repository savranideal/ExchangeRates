using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Core.Export
{

    /// <summary>
    /// Export managerların nasıl üretileceğine karar verrir
    /// </summary>
   public class ExportFactory
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static JsonExportManager CreateJsonExportManager()
        {

            return Singleton<JsonExportManager>.Instance;
        }
        public static XmlExportManager CreateXmlExportManager()
        {

            return Singleton<XmlExportManager>.Instance;
        }
        public static CsvExportManager CreateCsvExportManager()
        {

            return Singleton<CsvExportManager>.Instance;
        }
    }
}
