using System;
using System.Collections.Concurrent;
using System.Threading;

namespace ExchangeRates
{
    /// <summary>
    /// Servis çağrımları için context
    /// </summary>
    public class ServiceFlowScope : IDisposable
    {
        private static readonly AsyncLocal<ServiceFlowScopeWrapper> ScopeContext = new AsyncLocal<ServiceFlowScopeWrapper>();

        /// <summary>
        /// Mevcut başlatılmış olan servis çağrımını döner. Eğer başlatılmamış ise null servis flow scope döner ki içerisinden dönen tüm itemlar nulldır.
        /// </summary>
        public static ServiceFlowScope Current
        {
            get
            {
                var wrapper = ScopeContext.Value;
                return wrapper != null && wrapper.Scope != null ? wrapper.Scope : Null;
            }

            set
            {
                ScopeContext.Value = value == null ? null : new ServiceFlowScopeWrapper(value);
            }
        }


        private sealed class ServiceFlowScopeWrapper
        {
            public readonly ServiceFlowScope Scope;

            public ServiceFlowScopeWrapper(ServiceFlowScope scope)
            {
                Scope = scope;
            }
        }

        private readonly ServiceFlowScope _parentScope;

        private ServiceFlowScope()
        {
            if (IsNull)
                return;

            if (!Current.IsNull)
                _parentScope = Current;

            Current = this;

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls        

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Current = _parentScope;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion

        /// <summary>
        /// Bir servis çağrımı context'i başlatır.
        /// </summary>
        /// <returns></returns>
        public static ServiceFlowScope Begin()
        {
            return new ServiceFlowScope();
        }

        /// <summary>
        /// Bu servis çağrımı içindeki request
        /// </summary>
        public virtual string Request { get; set; }

        /// <summary>
        /// Bu servis çağrımı içindeki response
        /// </summary>
        public virtual string Response { get; set; }

        /// <summary>
        /// Bu servis çağrımı içindeki request başlığı
        /// </summary>
        public virtual string RequestHeaders { get; set; }

        /// <summary>
        /// Bu servis çağrımı içindeki response başlığı
        /// </summary>
        public virtual string ResponseHeaders { get; set; }

        private ConcurrentDictionary<string, object> _items;
        /// <summary>
        /// Bu servis çağrımı içindeki ekstra saklanan bilgiler.
        /// </summary>
        public virtual ConcurrentDictionary<string, object> Items => _items ?? (_items = new ConcurrentDictionary<string, object>());

        public virtual bool IsNull => false;


        private static NullServiceFlowScope Null => NullServiceFlowScope.Instance;

        private sealed class NullServiceFlowScope : ServiceFlowScope
        {
            public override bool IsNull => true;

            private static readonly ReaderWriterLockSlim _instanceLocker = new ReaderWriterLockSlim();
            private static NullServiceFlowScope _instance = new NullServiceFlowScope();
            public static NullServiceFlowScope Instance
            {
                get
                {
                    using (_instanceLocker.ReadLock())
                        return _instance;
                }
            }

#pragma warning disable S3237 // "value" parameters should be used
            public override string Request { get => null; set { /* intentionally left blank */ } }
            public override string Response { get => null; set { /* intentionally left blank */ } }
            public override string RequestHeaders { get => null; set { /* intentionally left blank */ } }
            public override string ResponseHeaders { get => null; set { /* intentionally left blank */ } }
#pragma warning restore S3237 // "value" parameters should be used
            public override ConcurrentDictionary<string, object> Items => new ConcurrentDictionary<string, object>(base.Items);

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    //yanlışlıkla null scope dispose edilirse diye..
                    using (_instanceLocker.WriteLock())
#pragma warning disable S2696 // Instance members should not write to "static" fields
                        _instance = new NullServiceFlowScope();
#pragma warning restore S2696 // Instance members should not write to "static" fields
                }
            }
        }
    }


}
