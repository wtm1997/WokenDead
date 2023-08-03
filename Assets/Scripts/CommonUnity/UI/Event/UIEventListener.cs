using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventListener
{

    public delegate void VoidDelegate(GameObject go);
    public delegate void IntDelegate(GameObject go, int value);
    public delegate void BoolDelegate(GameObject go, bool value);
    public delegate void PointerEventDataDelegate(GameObject go, PointerEventData data);

    public static IEventTrigger<VoidDelegate> OnClick(GameObject go)
    {
        return ClickEventTrigger.Get(go);
    }

    public static IEventTrigger<IntDelegate> OnDoubleClick(GameObject go)
    {
        return DoubleClickEventTrigger.Get(go);
    }
}

public interface IEventTrigger<T>
{
    void AddListener(T t);
    void RemoveListener();
    void RemoveListener(T t);
}
