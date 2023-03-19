namespace ScreenManager.Screens
{
    using System;
    using global::ScreenManager.Popups;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameplayScreen : BaseScreen
    {
        [SerializeField] private Slider   healthBarPlayer;
        [SerializeField] private TMP_Text reviveTime;
        [SerializeField] private TMP_Text countDownTimer;

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

        public void OnTimerUpdate(bool isRunning, float timeLeft)
        {
            var timespan = TimeSpan.FromSeconds(timeLeft);
            this.countDownTimer.text = isRunning ? $"{timespan.Minutes:00}:{timespan.Seconds:00}" : "00:00";
        }

        public void OnTimerEnd()
        {
            
        }
    }
}