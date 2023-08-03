using CommonUnity.Extension;
using UnityEngine;

namespace CommonUnity.UI
{
    public interface IUIPanel
    {
        bool IsActive();
        void SetAlpha(float value);
        void OnCreate();
        void OnOpen();
        void OnUpdate();
        void OnLateUpdate();
        void OnClose();
        void OnDelete();
    }

    public abstract class UIPanel : MonoBehaviour, IUIPanel
    {
        private void Awake()
        {
            InitComps();
        }

        protected CanvasGroup canvasGroup;

        private void InitComps()
        {
            canvasGroup = gameObject.EnsureComponent<CanvasGroup>();
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

        public void SetBlockRaycasts(bool value)
        {
            canvasGroup.blocksRaycasts = value;
        }

        public virtual void OnCreate()
        {
        }

        public virtual void OnOpen()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnLateUpdate()
        {
        }

        public virtual void OnClose()
        {
        }

        public virtual void OnDelete()
        {
        }

        public void CloseSelf()
        {
            SetAlpha(0f);
        }
    }
}