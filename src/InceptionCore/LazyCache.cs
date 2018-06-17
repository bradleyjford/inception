using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace InceptionCore
{
    public abstract class LazyCache<TKey, TItem> :
        IEnumerable<KeyValuePair<TKey, TItem>>,
        IDisposable
    {
        readonly ConcurrentDictionary<TKey, TItem> _dictionary = new ConcurrentDictionary<TKey, TItem>();

        bool _isDisposed;

        public TItem this[TKey key]
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("LazyCache");
                }

                return _dictionary.GetOrAdd(key, CreateItem);
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public IEnumerator<KeyValuePair<TKey, TItem>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        protected abstract TItem CreateItem(TKey key);


        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _isDisposed = true;

                foreach (var item in _dictionary)
                {
                    var disposable = item as IDisposable;

                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }
    }
}