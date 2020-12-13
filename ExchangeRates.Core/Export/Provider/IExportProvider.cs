using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Core.Export
{
    /// <summary>
    /// Export işlemlerini temsil eder.
    /// </summary>
    public interface IExportProvider
    {
    }



    /// <summary>
    ///   export işemini <see cref="TOutput"/> olarak sağlar
    /// </summary>
    public interface IExportProvider<TOutput> : IExportProvider
        where TOutput : ExportResult
    {
         /// <summary>
         ///
         /// </summary>
         /// <returns></returns>
        Task<TOutput> Export();
    }
     
    /// <summary>
    ///   <see cref="TInput"/> export işemini <see cref="TOutput"/> olarak sağlar
    /// </summary>
    public interface IExportProvider<TInput, TOutput> : IExportProvider
        where TInput : class
        where TOutput : ExportResult
    { 
        Task<TOutput> Export(TInput input); 
    }





}
