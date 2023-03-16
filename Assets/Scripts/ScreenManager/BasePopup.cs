namespace ScreenManager
{
    using global::ScreenManager.Screens;

    public abstract class BasePopup : BaseScreenAgent
    {
        public void BringToFront()
        {
            this.transform.SetAsLastSibling();
        }
    }
}