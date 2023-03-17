namespace ScreenManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _screens;
        [SerializeField] private Transform        _screenTransform;
        [SerializeField] private Transform        _popupTransform;
        [SerializeField] private GameObject       _unInteractableBG;
        public static            ScreenManager    Instance { get; private set; }

        private Dictionary<Type, IScreen> TypeToScreen { get; } = new();

        public BaseScreen CurrentScreen { get; set; }

        public bool IsAnyPopupShowed
        {
            get => this._unInteractableBG.activeSelf;
            set => this._unInteractableBG.SetActive(value);
        }

        private void Awake()
        {
            Instance = this;

            foreach (var prefab in this._screens)
            {
                var spawnedScreen = Instantiate(prefab);
                var screen        = spawnedScreen.GetComponent<IScreen>();
                screen.ScreenManager = this;

                if (this.CurrentScreen is not null || screen is BasePopup) screen.Hide();

                var rectTransform = spawnedScreen.GetComponent<RectTransform>();

                if (screen is BaseScreen baseScreen)
                {
                    spawnedScreen.transform.SetParent(this._screenTransform);
                    this.CurrentScreen = baseScreen;
                }
                else if (screen is BasePopup)
                {
                    spawnedScreen.transform.SetParent(this._popupTransform);
                    screen.OnStateChange += this.OnStateChange;
                }

                rectTransform.localPosition = Vector3.Scale(rectTransform.localPosition, new Vector3(1, 1, 0));

                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);

                rectTransform.anchorMax  = Vector2.one;
                rectTransform.anchorMin  = Vector2.zero;
                rectTransform.pivot      = Vector2.one / 2.0f;
                rectTransform.rotation   = Quaternion.identity;
                rectTransform.localScale = Vector3.one;

                this.TypeToScreen.Add(screen.GetType(), screen);
            }
        }

        private void OnStateChange(bool state)
        {
            this.IsAnyPopupShowed = this.TypeToScreen.Values.Any(screen => screen is BasePopup { IsVisible: true });
            if (!this.IsAnyPopupShowed) this.CurrentScreen.Show();
        }

        public void OpenScreen<T>() where T : IScreen
        {
            var screen = this.GetScreen<T>();

            if (screen is null) return;

            if (screen is BaseScreen baseScreen)
            {
                this.CurrentScreen = baseScreen;
                foreach (var s in this.TypeToScreen.Values)
                    if (s is BaseScreen)
                        s.Hide();

                screen.Show();
            }
            else if (screen is BasePopup popup)
            {
                screen.Show();
                this.CurrentScreen.Interactable = false;
                popup.BringToFront();
            }
        }

        public T GetScreen<T>() where T : IScreen
        {
            if (this.TypeToScreen.TryGetValue(typeof(T), out var screen)) return (T)screen;

            Debug.LogError($"{typeof(T).Name} not found!");
            return default;
        }
    }
}