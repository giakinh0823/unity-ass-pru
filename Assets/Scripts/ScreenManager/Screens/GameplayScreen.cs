namespace ScreenManager.Screens
{
    using global::ScreenManager.Popups;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameplayScreen : BaseScreen
    {
        [SerializeField] private Slider healthBarPlayer;

        public void SetHealthPercent(float percent)
        {
            this.healthBarPlayer.value = percent;
        }

        public void Pause()
        {
            Time.timeScale = 0;
            this.ScreenManager.OpenScreen<PausePopup>();
        }
    }
}