using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickOrDragEventTrigger : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerClickHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IEventTrigger<UIEventListener.VoidDelegate>
{
    public UIEventListener.VoidDelegate onClick;
    public UIEventListener.PointerEventDataDelegate onDrag;
    public UIEventListener.PointerEventDataDelegate onEndDrag;

    public float clickLimitTime = 0.8f;
    public float clickPrepareTime = 0.2f;
    public Image imgProgress;

    private float _lastDownTime;
    private bool _checkDrag;
    private bool _canDrag;

    public static IEventTrigger<UIEventListener.VoidDelegate> Get(GameObject go)
    {
        ClickOrDragEventTrigger listener = go.GetComponent<ClickOrDragEventTrigger>();
        if (listener == null) listener = go.AddComponent<ClickOrDragEventTrigger>();
        return listener;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _lastDownTime = Time.realtimeSinceStartup;
        _checkDrag = true;
        _canDrag = false;

        //pos
        imgProgress.transform.position = transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _checkDrag = false;
        imgProgress.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_canDrag)
        {
            return;
        }

        onClick(gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _checkDrag = false;
        imgProgress.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_canDrag)
        {
            return;
        }

        onDrag(gameObject, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onEndDrag(gameObject, eventData);
    }

    private void Update()
    {
        if (_checkDrag)
        {
            float progress = (Time.realtimeSinceStartup - _lastDownTime) / clickLimitTime;
            progress -= clickPrepareTime;

            if (progress > 0f)
            {
                imgProgress.fillAmount = progress;
                imgProgress.gameObject.SetActive(true);
            }
            if (progress >= 1f)
            {
                _canDrag = true;
                imgProgress.gameObject.SetActive(false);
            }
        }
    }

    public void AddListener(UIEventListener.VoidDelegate t)
    {
        onClick += t;
    }

    public void RemoveListener()
    {
        onClick = null;
    }

    public void RemoveListener(UIEventListener.VoidDelegate t)
    {
        onClick -= t;
    }
}