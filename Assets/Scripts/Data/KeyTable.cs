using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Stella.Data
{

    public interface IKey<T>
    {
        T Key { get; }
    }

    public interface IKeyTable<TKey, TValue> 
        where TValue : IKey<TKey>
    {
        IReadOnlyList<TValue> Values { get; }
        
        IEnumerable<TKey> Keys { get; }

        // void SetValues(IEnumerable<TValue> values);
    }
    
    public class KeyTable<TKey, TValue> : ScriptableObject, IKeyTable<TKey, TValue> 
        where TValue : IKey<TKey>
    {
        public int Count => values.Count;
        
        public IReadOnlyList<TValue> Values => values;
        public IEnumerable<TKey> Keys => values.Select(v => v.Key);
        
        [TableList(AlwaysExpanded = true)]
        [SerializeField]
        protected List<TValue> values = null;

        public void Add(TValue value)
        {
            values.Add(value);
        }

        public bool Contains(TKey key) => Keys.Contains(key);
        public bool Contains(TValue value) => values.Contains(value);

        public TValue GetValue(TKey key)
        {
            foreach (var value in values)
            {
                if (value.Key.Equals(key))
                {
                    return value;
                }
            }

            return default;
        }

        public void Remove(TKey key)
        {
            for (var i = values.Count - 1; i >= 0; i--)
            {
                var value = values[i];
                if (value.Key.Equals(key))
                {
                    values.Remove(value);
                }
            }
        }

        public void Clear()
        {
            values.Clear();
        }
    }
}