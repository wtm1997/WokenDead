using UnityEngine;
using UnityEngine.EventSystems;

namespace CommonUnity.UI
{
    public class ButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Vector3 oldScale = new Vector3(1, 1, 1);
        public Vector3 newScale = new Vector3(1.1f, 1.1f, 1f);
        public float duration = 0.2f;

        private bool _down;
        private bool _up;
        private float _timer;

        private void OnEnable()
        {
            _down = false;
            _up = false;
            _timer = 0f;
            transform.localScale = oldScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _down = true;
            _up = false;
            _timer = 0f;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _down = false;
            _up = true;
            _timer = 0f;
        }

        private void Update()
        {
            if (_down)
            {
                _timer += Time.unscaledDeltaTime;
                transform.localScale = Vector3.Lerp(transform.localScale, newScale, _timer / duration);
                if (_timer >= duration)
                {
                    _down = false;
                }
            }

            if (_up)
            {
                _timer += Time.unscaledDeltaTime;
                transform.localScale = Vector3.Lerp(transform.localScale, oldScale, _timer / duration);
                if (_timer >= duration)
                {
                    _up = false;
                }
            }
        }
    }
}