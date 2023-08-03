using UnityEngine;

namespace CommonUnity.UI
{
    public interface IUIItem
    {
        bool IsActive();
        void SetAlpha(float value);
    }

    public class UIItem : MonoBehaviour, IUIItem
    {
        private void Awake()
        {
            InitComps();
        }

        protected CanvasGroup canvasGroup;

        private void InitComps()
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        public bool IsActive()
        {
            return canvasGroup.alpha >= 0.99f;
        }

        public void SetAlpha(float value)
        {
            canvasGroup.alpha = value;
            canvasGroup.blocksRaycasts = value >= 1f;
        }
    }
}
