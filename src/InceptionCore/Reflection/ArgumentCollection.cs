using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InceptionCore.Reflection
{
    public class ArgumentCollection : IEnumerable<KeyValuePair<string, object>>
    {
        readonly ArgumentKeyedCollection _arguments = new ArgumentKeyedCollection();

        public int Count => _arguments.Count;

        public KeyValuePair<string, object> this[int index] => _arguments[index].ToKeyValuePair();

        public object this[string name] => _arguments[name].Value;

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var argument in _arguments)
            {
                yield return argument.ToKeyValuePair();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(string name, object value)
        {
            _arguments.Add(new Argument(name, value));
        }

        public void Insert(int index, string name, object value)
        {
            _arguments.Insert(index, new Argument(name, value));
        }

        public bool Contains(string name)
        {
            return _arguments.Contains(name);
        }

        public Type[] GetArgumentTypes()
        {
            var result = new Type[_arguments.Count];

            for (var i = 0; i < _arguments.Count; i++)
            {
                result[i] = _arguments[i].Value.GetType();
            }

            return result;
        }

        public bool TryGetValue(string name, out object value)
        {
            if (_arguments.Contains(name))
            {
                value = _arguments[name].Value;

                return true;
            }

            value = null;

            return false;
        }

        struct Argument
        {
            public readonly string Name;
            public readonly object Value;

            public Argument(string name, object value)
            {
                Name = name;
                Value = value;
            }

            public KeyValuePair<string, object> ToKeyValuePair()
            {
                return new KeyValuePair<string, object>(Name, Value);
            }
        }

        class ArgumentKeyedCollection : KeyedCollection<string, Argument>
        {
            protected override string GetKeyForItem(Argument item)
            {
                return item.Name;
            }
        }
    }
}