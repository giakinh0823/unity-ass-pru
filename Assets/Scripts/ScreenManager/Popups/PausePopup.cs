namespace ScreenManager.Popups
{
    using Model;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class PausePopup : BasePopup
    {
        public void Resume()
        {
            this.Hide();
            Time.timeScale = 1;
        }

        public void SaveAndExit()
        {
            Time.timeScale = 1;
            PlayerLocalData.Instance.Save();
            SceneManager.LoadScene("Main Menu");
        }

        public void Setting()
        {
            this.ScreenManager.OpenScreen<SettingPopup>();
        }
    }
}