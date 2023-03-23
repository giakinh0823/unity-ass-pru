namespace ScreenManager.Popups
{
    using UnityEngine;

    public class QuitPopup : BasePopup
    {
        public void Quit()
        {
            Application.Quit();
        }
    }
}