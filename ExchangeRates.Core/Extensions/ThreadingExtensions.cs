using System;
using System.Threading;

namespace ExchangeRates
{
    public static class ThreadingExtensions
    {
        private enum LockKind
        {
            Read,
            Write,
            UpgradeableRead
        }

        /// <summary>
        /// Kullanılacak olan <see cref="ReaderWriterLockSlim"/> objelerinin exit lock yaptığına emin olmak için oluşturulan sınıf.
        /// </summary>
        private sealed class Locker : IDisposable
        {
            private readonly ReaderWriterLockSlim _locker;
            private readonly LockKind _kind;

            public Locker(ReaderWriterLockSlim locker, LockKind kind)
            {
                _locker = locker;
                _kind = kind;
                switch (_kind)
                {
                    case LockKind.Read:
                        locker.EnterReadLock();
                        break;
                    case LockKind.Write:
                        locker.EnterWriteLock();
                        break;
                    case LockKind.UpgradeableRead:
                        locker.EnterUpgradeableReadLock();
                        break;
                    default:
                        break;
                }
            }

            public void Dispose()
            {
                switch (_kind)
                {
                    case LockKind.Read:
                        _locker.ExitReadLock();
                        break;
                    case LockKind.Write:
                        _locker.ExitWriteLock();
                        break;
                    case LockKind.UpgradeableRead:
                        _locker.ExitUpgradeableReadLock();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Okumaları locklar. Using ile kullanılır.
        /// </summary>
        /// <param name="locker"></param>
        /// <returns></returns>
        public static IDisposable ReadLock(this ReaderWriterLockSlim locker)
        {
            return new Locker(locker, LockKind.Read);
        }

        /// <summary>
        /// Yazmaları locklar. Using ile kullanılır.
        /// </summary>
        /// <param name="locker"></param>
        /// <returns></returns>
        public static IDisposable WriteLock(this ReaderWriterLockSlim locker)
        {
            return new Locker(locker, LockKind.Write);
        }

        /// <summary>
        /// Okumaları locklar, ileride yazma lockına dönüştürülebilir. Using ile kullanılır.
        /// </summary>
        /// <param name="locker"></param>
        /// <returns></returns>
        public static IDisposable UpgradeableReadLock(this ReaderWriterLockSlim locker)
        {
            return new Locker(locker, LockKind.UpgradeableRead);
        }
    }


}
