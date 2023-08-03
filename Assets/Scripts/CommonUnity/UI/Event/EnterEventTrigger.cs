using UnityEngine;
using UnityEngine.EventSystems;

public class EnterEventTrigger : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IEventTrigger<UIEventListener.BoolDelegate>
{
    public UIEventListener.BoolDelegate onEnter;

    public static IEventTrigger<UIEventListener.BoolDelegate> Get(GameObject go)
    {
        var listener = go.GetComponent<EnterEventTrigger>();
        if (listener == null) listener = go.AddComponent<EnterEventTrigger>();
        return listener;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onEnter(gameObject, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onEnter(gameObject, false);
    }

    public void AddListener(UIEventListener.BoolDelegate t)
    {
        onEnter += t;
    }

    public void RemoveListener(UIEventListener.BoolDelegate t)
    {
        onEnter -= t;
    }

    public void RemoveListener()
    {
        onEnter = null;
    }
}
