using System;
using System.Collections;
using System.Collections.Generic;

namespace CommonLogic
{

    public interface ILinkNode<T> where T : class
    {
        T Prev { get; set; }
        T Next { get; set; }
    }

    public class LinkDictionary<TKey, TValue> : IEnumerable<TValue>
        where TValue : class, ILinkNode<TValue>
    {
        private readonly Dictionary<TKey, TValue> _dict
            = new Dictionary<TKey, TValue>();

        private TValue _headPtr;
        private TValue _tailPtr;

        public int Count => _dict.Count;

        public bool ContainsKey(TKey key)
        {
            return _dict.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dict.TryGetValue(key, out value);
        }

        public void Add(TKey key, TValue obj)
        {
            if (_dict.ContainsKey(key))
            {
                return;
            }

            _dict.Add(key, obj);

            _headPtr ??= obj;

            if (_tailPtr == null)
            {
                _tailPtr = obj;
            }
            else
            {
                _tailPtr.Next = obj;
                obj.Prev = _tailPtr;
                _tailPtr = obj;
            }
        }

        public void Remove(TKey key)
        {
            if (!_dict.TryGetValue(key, out var obj))
            {
                return;
            }

            _dict.Remove(key);

            Remove(obj);
        }

        private void Remove(TValue value)
        {
            var next = value.Next;
            var pre = value.Prev;

            if (pre != null)
            {
                pre.Next = next;
            }
            if (next != null)
            {
                next.Prev = pre;
            }
            if (value == _headPtr)
            {
                _headPtr = next;
            }
            if (value == _tailPtr)
            {
                _tailPtr = pre;
            }

            value.Next = null;
            value.Prev = null;
        }

        public void Clear()
        {
            _dict.Clear();

            var curNode = _headPtr;
            while (curNode != null)
            {
                var nextNode = curNode.Next;
                Remove(curNode);
                curNode = nextNode;
            }
            _headPtr = null;
            _tailPtr = null;
        }

        private readonly Enumerator _enumerator = new Enumerator();

        public IEnumerator<TValue> GetEnumerator()
        {
            _enumerator.Set(_headPtr, _tailPtr);
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class Enumerator : IEnumerator<TValue>
        {
            private TValue _head;
            private TValue _tail;
            private TValue _current;

            public void Set(TValue head, TValue tail)
            {
                _head = head;
                _tail = tail;
            }

            public TValue Current => _current;
            object IEnumerator.Current => _current;

            public bool MoveNext()
            {
                if (_current == null || _current == _tail)
                {
                    return false;
                }

                if (_current == _head)
                {
                    return true;
                }

                _current = _current.Next;
                return true;
            }

            public void Reset()
            {
                _current = _head;
            }

            public void Dispose()
            {
                _head = null;
                _tail = null;
                _current = null;
            }
        }
    }

}
