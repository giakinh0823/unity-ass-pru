namespace ScreenManager
{
    using System;

    public interface IScreen
    {
        public ScreenManager ScreenManager { get; set; }

        public object Data         { get; set; }
        public bool   IsVisible    { get; set; }
        public bool   Interactable { get; set; }

        public Action<bool> OnStateChange { get; set; }


        void Show();
        void Hide();
        void OnShow();
        void OnHide();
        void Init();
    }
}