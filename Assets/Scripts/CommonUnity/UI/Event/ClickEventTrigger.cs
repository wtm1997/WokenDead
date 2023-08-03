using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickEventTrigger : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler,
    IEventTrigger<UIEventListener.VoidDelegate>
{
    public UIEventListener.VoidDelegate onClick;
    public float clickInterval = 0.1f;

    private float _lastClickTime;
    private static GameObject _clickObj;

    public static IEventTrigger<UIEventListener.VoidDelegate> Get(GameObject go)
    {
        _clickObj = go;
        ClickEventTrigger listener = go.GetComponent<ClickEventTrigger>();
        if (listener == null) listener = go.AddComponent<ClickEventTrigger>();
        return listener;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.realtimeSinceStartup - _lastClickTime < clickInterval)
        {
            return;
        }
        if (onClick != null)
        {
            _lastClickTime = Time.realtimeSinceStartup;
            onClick(gameObject);
        }
    }

    public void AddListener(UIEventListener.VoidDelegate t)
    {
        onClick += t;
    }

    public void RemoveListener(UIEventListener.VoidDelegate t)
    {
        onClick -= t;
    }

    public void RemoveListener()
    {
        onClick = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _clickObj.transform.DOScale(0.9f, 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_clickObj.transform.DOScale(1.1f, 0.05f));
        seq.Append(_clickObj.transform.DOScale(1f, 0.05f));
        seq.SetUpdate(true);
    }
}