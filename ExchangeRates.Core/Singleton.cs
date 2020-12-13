using System;

namespace ExchangeRates.Core
{
    /// <summary>
    /// Thread-safe ve lazy singleton base.
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public abstract class Singleton<TClass>
        where TClass : class
    {
        private static readonly Lazy<TClass> _lazy = new Lazy<TClass>(CreateInstance);

        /// <summary>
        /// 
        /// </summary>
        public static TClass Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        private static TClass CreateInstance()
        {
            return Activator.CreateInstance(typeof(TClass), true) as TClass;
        }
    }
}
