using System;
using System.Collections.Generic;

namespace CommonLogic
{
    public class MultiDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, List<TValue>> _dict
            = new Dictionary<TKey, List<TValue>>();

        public void Add(TKey key, TValue value)
        {
            if (!_dict.TryGetValue(key, out var list))
            {
                list = new List<TValue>();
                _dict.Add(key, list);
            }

            if (!list.Contains(value))
            {
                list.Add(value);
            }
        }

        public bool TryGet(TKey key, out List<TValue> list)
        {
            return _dict.TryGetValue(key, out list);
        }

        public void Remove(TKey key, TValue value)
        {
            if (!_dict.TryGetValue(key, out var list))
            {
                return;
            }

            list.Remove(value);
        }

        public void Clear(Action<TValue> onBeforeRemove)
        {
            foreach (var kv in _dict)
            {
                var list = kv.Value;
                foreach (var value in list)
                {
                    onBeforeRemove(value);
                }
            }

            _dict.Clear();
        }
    }
}