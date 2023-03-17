namespace ScreenManager.Screens
{
    using global::ScreenManager.Popups;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameplayScreen : BaseScreen
    {
        [SerializeField] private Slider   healthBarPlayer;
        [SerializeField] private TMP_Text reviveTime;

        public float HealthPercent
        {
            set => this.healthBarPlayer.value = value;
            get => this.healthBarPlayer.value;
        }

        public int ReviveTime
        {
            set => this.reviveTime.text = $"X{value}";
        }

        public void Pause()
        {
            Time.timeScale = 0;
            this.ScreenManager.OpenScreen<PausePopup>();
        }
    }
}