namespace ScreenManager.Popups
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class DeadPopup : BasePopup
    {
        public PlayerController PlayerController => this.Data as PlayerController;

        public void NewGame()
        {
            SceneManager.LoadScene("Main Menu");
        }

        public void Revive()
        {
            this.Hide();
            this.PlayerController.ReviveTime--;
            this.PlayerController.ReHealth();
        }

        public override void OnShow()
        {
            base.OnShow();
            Time.timeScale = 0;
        }

        public override void OnHide()
        {
            base.OnHide();
            Time.timeScale = 1;
        }
    }
}