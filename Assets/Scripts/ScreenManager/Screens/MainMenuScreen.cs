namespace ScreenManager.Screens
{
    using global::ScreenManager.Popups;
    using UnityEngine.SceneManagement;

    public class MainMenuScreen : BaseScreen
    {
        public void OnClickNewGameBtn()
        {
            SceneManager.LoadScene("Game");
        }

        public void OnClickQuitBtn()
        {
            this.ScreenManager.OpenScreen<QuitPopup>();
        }

        public void OnClickSettingBtn()
        {
            this.ScreenManager.OpenScreen<SettingPopup>();
        }

        public void OnClickResumeBtn()
        {
            // TODO: 
        }
    }
}