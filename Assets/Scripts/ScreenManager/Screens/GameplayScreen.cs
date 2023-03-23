namespace ScreenManager.Screens
{
    using System;
    using global::ScreenManager.Popups;
    using Model;
    using TMPro;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.Serialization;
    using UnityEngine.UI;

    public class GameplayScreen : BaseScreen
    {
        [SerializeField] private Slider   healthBarPlayer;
        [SerializeField] private TMP_Text reviveTime;
        [SerializeField] private TMP_Text countDownTimer;
        [SerializeField] private TMP_Text currentLevelText;
        [SerializeField] private TMP_Text currentCoin;

        public float HealthPercent
        {
            set => this.healthBarPlayer.value = value;
            get => this.healthBarPlayer.value;
        }

        public int ReviveTime
        {
            set => this.reviveTime.text = $"X{value}";
        }

        public int CurrentLevel
        {
            set => this.currentLevelText.text = $"Level {value}";
        }

        public int CurrentCoin
        {
            set => this.currentCoin.text = $"{value}";
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
            // TODO: Player will die and back to level 1
            // But for now, just take him to the main menu
            SceneManager.LoadScene("Main Menu");
        }

        public override void OnShow()
        {
            base.OnShow();
            this.ReviveTime   = PlayerLocalData.Instance.CurrentPlayerReviveTime;
            this.CurrentLevel = PlayerLocalData.Instance.CurrentPlayerLevel;
            this.CurrentCoin  = PlayerLocalData.Instance.CurrentCoin;
        }

        public void CoinUpdate()
        {
            this.CurrentCoin = PlayerLocalData.Instance.CurrentCoin;
        }
    }
}