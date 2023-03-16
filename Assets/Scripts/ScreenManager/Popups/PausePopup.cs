namespace ScreenManager.Popups
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class PausePopup : BasePopup
    {
        public void Resume()
        {
            this.Hide();
            Time.timeScale = 1;
        }

        public void BackToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main Menu");
        }

        public void Setting()
        {
            this.ScreenManager.OpenScreen<SettingPopup>();
        }
    }
}