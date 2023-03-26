namespace ScreenManager.Screens
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(CanvasGroup), typeof(GraphicRaycaster))]
    public abstract class BaseScreenAgent : MonoBehaviour, IScreen
    {
        private CanvasGroup      _canvasGroup;
        private GraphicRaycaster _graphicRaycaster;

        private void Awake()
        {
            this._canvasGroup      = this.GetComponent<CanvasGroup>();
            this._graphicRaycaster = this.GetComponent<GraphicRaycaster>();
            this.Init();
        }

        public bool          IsInitialized { get; set; }
        public ScreenManager ScreenManager { get; set; }
        public object        Data          { get; set; }

        public bool IsVisible
        {
            get => this.gameObject.activeSelf;
            set => this.gameObject.SetActive(value);
        }

        public bool Interactable
        {
            get => this._graphicRaycaster.enabled;
            set => this._graphicRaycaster.enabled = value;
        }

        public Action<bool> OnStateChange { get; set; }

        public void Show()
        {
            if (this.IsVisible && this.Interactable) return;
            this.IsVisible    = true;
            this.Interactable = true;
            if (this.IsInitialized) this.OnShow();
            // _canvasGroup.alpha = 1.0f;
            // _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            if (!this.IsVisible) return;
            this.IsVisible    = false;
            this.Interactable = false;
            if (this.IsInitialized) this.OnHide();
            // _canvasGroup.alpha = 0.0f;
            // _canvasGroup.blocksRaycasts = false;
        }

        public virtual void Init()
        {
        }

        public virtual void OnShow()
        {
            this.OnStateChange?.Invoke(true);
        }

        public virtual void OnHide()
        {
            this.OnStateChange?.Invoke(false);
        }
    }
}