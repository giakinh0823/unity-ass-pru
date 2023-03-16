namespace ScreenManager.Screens
{
    using global::ScreenManager.Popups;
    using UnityEngine;

    public class GameplayScreen : BaseScreen
    {
        public void Pause()
        {
            Time.timeScale = 0;
            this.ScreenManager.OpenScreen<PausePopup>();
        }
    }
}