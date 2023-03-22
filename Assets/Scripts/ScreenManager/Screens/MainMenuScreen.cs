namespace ScreenManager.Screens
{
    using global::ScreenManager.Popups;
    using Model;
    using TMPro;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class MainMenuScreen : BaseScreen
    {
        [SerializeField] private TMP_Text highScoreText;

        public override void OnShow()
        {
            this.highScoreText.text = PlayerLocalData.Instance.HighestLevel > 1 ? $"Highest Level: {PlayerLocalData.Instance.HighestLevel}" : "";
        }

        public void OnClickNewGameBtn()
        {
            PlayerLocalData.Instance.Init();
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
            SceneManager.LoadScene("Game");
        }
    }
}