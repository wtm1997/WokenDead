using System;

namespace CommonUnity.UI
{
    public class BindableProperty<T> where T : IComparable<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (_value.CompareTo(value) != 0)
                {
                    _value = value;
                    _onValueChanged?.Invoke(value);
                }
            }
        }

        private Action<T> _onValueChanged;

        public void RegisterOnValueChanged(Action<T> onValueChanged)
        {
            _onValueChanged += onValueChanged;
        }

        public void UnRegisterOnValueChanged(Action<T> onValueChanged)
        {
            _onValueChanged -= onValueChanged;
        }
    }
}