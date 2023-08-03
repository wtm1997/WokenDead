using System;
using System.Collections.Generic;

namespace CommonLogic
{

    public class HeapItem : IComparable, IDisposable
    {
        public int heapIndex = -1;
        public int insertionIndex = -1;

        public bool IsInHeap() => heapIndex >= 0;

        public virtual int CompareTo(object obj)
        {
            // < 0 : 该实例按排序顺序在obj之前
            //== 0 : 该实例按排序顺序和obj一样
            // > 0 : 该实例按排序顺序在obj之后

            var item = obj as HeapItem;
            if (item == null)
                return 1;
            return insertionIndex - item.insertionIndex;
        }

        public virtual void Dispose()
        {
            heapIndex = -1;
            insertionIndex = -1;
        }
    }

    //优先级序列
    public enum EHeapCheckPriorityMethod
    {
        Custom = 0,
        Greater,
        Less,
    }

    public class Heap<T> where T : HeapItem
    {
        public delegate bool CheckPriorityFunc(T higher, T lower);

        public delegate int CompareFunc(T higher, T lower);

        private readonly List<T> _data = new List<T>();
        private int _itemEverEnqueued = 0;
        private readonly CheckPriorityFunc _checkPriorityFunc = null;
        private readonly CompareFunc _compareFunc = null;

        public int Count => _data.Count;
        public bool IsEmpty => _data.Count == 0;

        public Heap(CompareFunc compareFunc)
        {
            _compareFunc = compareFunc;
            _checkPriorityFunc = CheckPriorityByComparer;
        }

        public Heap(EHeapCheckPriorityMethod cpm, CompareFunc compareFunc = null)
        {
            switch (cpm)
            {
                case EHeapCheckPriorityMethod.Custom:
                    {
                        _compareFunc = compareFunc;
                        _checkPriorityFunc = CheckPriorityByComparer;
                    }
                    break;
                case EHeapCheckPriorityMethod.Greater:
                    {
                        _checkPriorityFunc = CheckPriorityByGreater;
                    }
                    break;
                case EHeapCheckPriorityMethod.Less:
                    {
                        _checkPriorityFunc = CheckPriorityByLess;
                    }
                    break;
            }
        }

        public void Build()
        {
            var lastPhi = Parent(_data.Count - 1);
            for (var hi = lastPhi; hi >= 0; --hi)
                CascadeDown(hi);
        }

        public void Clear()
        {
            for (var hi = 0; hi < _data.Count; ++hi)
                _data[hi].heapIndex = -1;
            _data.Clear();
        }

        public T Peek()
        {
            if (_data.Count > 0)
                return _data[0];
            return null;
        }

        public T GetAt(int hi)
        {
            if (hi >= 0 && hi < _data.Count)
                return _data[hi];
            return null;
        }

        public bool Contains(T item)
        {
            return item.heapIndex >= 0;
        }

        public void Enqueue(T item)
        {
            var hi = item.heapIndex;
            if (hi >= 0)
            {
                UpdatePriorityByIndex(hi);
                return;
            }
            else
            {
                hi = _data.Count;
                item.heapIndex = hi;
                item.insertionIndex = _itemEverEnqueued++;
                _data.Add(item);
                CascadeUp(hi);
            }
        }

        public T Dequeue()
        {
            var size = _data.Count;
            if (size == 0)
                return null;
            var item = _data[0];
            item.heapIndex = -1;
            if (size == 1)
            {
                _data.RemoveAt(0);
            }
            else
            {
                var temp = _data[size - 1];
                _data[0] = temp;
                temp.heapIndex = 0;
                _data.RemoveAt(size - 1);
                CascadeDown(0);
            }
            return item;
        }

        public void UpdatePriority(T item)
        {
            var hi = item.heapIndex;
            if (hi >= 0)
                UpdatePriorityByIndex(hi);
        }

        public void UpdatePriorityByIndex(int hi)
        {
            if (hi < 0 || hi >= _data.Count)
                return;
            var phi = Parent(hi);
            if (phi >= 0 && _checkPriorityFunc(_data[hi], _data[phi]))
                CascadeUp(hi);
            else
                CascadeDown(hi);
        }

        public void Remove(T item)
        {
            RemoveByIndex(item.heapIndex);
        }

        public void Remove(Func<T, bool> func)
        {
            for (var hi = 0; hi < _data.Count; ++hi)
            {
                var data = _data[hi];
                if (func(data))
                {
                    RemoveByIndex(data.heapIndex);
                    break;
                }
            }
        }

        public void RemoveByIndex(int hi)
        {
            if (hi < 0 || hi >= _data.Count)
                return;
            _data[hi].heapIndex = -1;
            var size = _data.Count;
            if (hi == (size - 1))
            {
                _data.RemoveAt(size - 1);
            }
            else
            {
                var temp = _data[size - 1];
                _data[hi] = temp;
                temp.heapIndex = hi;
                _data.RemoveAt(size - 1);
                UpdatePriorityByIndex(hi);
            }
        }

        public bool Check(T mustLessPriorityThanThis)
        {
            for (var hi = 0; hi < _data.Count; ++hi)
            {
                var item = _data[hi];
                if (item.heapIndex != hi)
                    return false;
                if (mustLessPriorityThanThis != null && _checkPriorityFunc(item, mustLessPriorityThanThis))
                    return false;
                var pi = Parent(hi);
                if (pi >= 0)
                {
                    if (_checkPriorityFunc(item, _data[pi]))
                        return false;
                }
            }
            return true;
        }

        //取父节点索引
        private int Parent(int i)
        {
            return (i - 1) >> 1;
        }

        //取左子节点索引
        private int LeftChild(int i)
        {
            return (i << 1) + 1;
        }

        //取右节点索引
        private int RightChild(int i)
        {
            return (i << 1) + 2;
        }

        //向上交换
        private void CascadeUp(int hi)
        {
            var phi = Parent(hi);
            while (phi >= 0)
            {
                if (_checkPriorityFunc(_data[hi], _data[phi]))
                {
                    Swap(hi, phi);
                    hi = phi;
                    phi = Parent(hi);
                }
                else
                    break;
            }
        }

        //向下交换
        private void CascadeDown(int hi)
        {
            var lhi = -1;
            var rhi = -1;
            var largest = hi;
            while (true)
            {
                lhi = LeftChild(hi);
                rhi = RightChild(hi);
                if (lhi < _data.Count && _checkPriorityFunc(_data[lhi], _data[largest]))
                    largest = lhi;
                if (rhi < _data.Count && _checkPriorityFunc(_data[rhi], _data[largest]))
                    largest = rhi;
                if (largest == hi)
                    break;
                Swap(hi, largest);
                hi = largest;
            }
        }

        //真实 交换两个元素
        private void Swap(int i, int j)
        {
            var tempI = _data[i];
            var tempJ = _data[j];
            _data[i] = tempJ;
            _data[j] = tempI;
            tempI.heapIndex = j;
            tempJ.heapIndex = i;
        }

        #region Helper
        private bool CheckPriorityByGreater(T higher, T lower)
        {
            var result = higher.CompareTo(lower);
            if (result > 0)
                return true;
            else if (result < 0)
                return false;
            else
                return higher.insertionIndex < lower.insertionIndex;
        }

        private bool CheckPriorityByLess(T higher, T lower)
        {
            var result = higher.CompareTo(lower);
            if (result < 0)
                return true;
            else if (result > 0)
                return false;
            else
                return higher.insertionIndex < lower.insertionIndex;
        }

        private bool CheckPriorityByComparer(T higher, T lower)
        {
            if (_compareFunc != null)
            {
                var result = _compareFunc(higher, lower);
                if (result < 0)
                    return true;
                else if (result > 0)
                    return false;
                else
                    return higher.insertionIndex < lower.insertionIndex;
            }
            return higher.insertionIndex < lower.insertionIndex;
        }
        #endregion

    }

}
