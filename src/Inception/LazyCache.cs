using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Inception
{
    public abstract class LazyCache<TKey, TItem> : 
        IEnumerable<KeyValuePair<TKey, TItem>>, 
        IDisposable
    {
        private readonly Dictionary<TKey, TItem> _dictionary = new Dictionary<TKey, TItem>();
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private bool _isDisposed;

        protected abstract TItem CreateItem(TKey key);

        public void Add(TKey key, TItem value)
        {
            if (_isDisposed) throw new ObjectDisposedException("LazyCache");

            _lock.EnterWriteLock();

            try
            {
                _dictionary.Add(key, value);
            }
            finally 
            {
                _lock.ExitWriteLock();
            }
        }

        public void Remove(TKey key)
        {
            if (_isDisposed) throw new ObjectDisposedException("LazyCache");

            _lock.EnterWriteLock();

            try
            {
                _dictionary.Remove(key);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public TItem this[TKey key]
        {
            get
            {
                if (_isDisposed) throw new ObjectDisposedException("LazyCache");

                _lock.EnterUpgradeableReadLock();

                try
                {
                    if (!_dictionary.ContainsKey(key))
                    {
                        _lock.EnterWriteLock();

                        try
                        {
                            if (!_dictionary.ContainsKey(key))
                            {
                                var item = CreateItem(key);

                                _dictionary.Add(key, item);
                            }
                        }
                        finally
                        {
                            _lock.ExitWriteLock();
                        }
                    }

                    return _dictionary[key];
                }
                finally
                {
                    _lock.ExitUpgradeableReadLock();
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, TItem>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _isDisposed = true;

                _lock.EnterWriteLock();

                try
                {
                    foreach (var item in _dictionary)
                    {
                        var disposable = item as IDisposable;

                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                }
                finally
                {
                    _lock.ExitWriteLock();
                    _lock.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
