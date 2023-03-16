namespace ScreenManager.Popups
{
    using UnityEngine;
    using UnityEngine.UI;

    public class SettingPopup : BasePopup
    {
        [SerializeField] private Slider soundSlider;

        public override void Init()
        {
            base.Init();
            this.soundSlider.onValueChanged.AddListener(OnSoundSliderChangeValue);
        }

        private static void OnSoundSliderChangeValue(float value)
        {
            AudioListener.volume = value;
        }
    }
}