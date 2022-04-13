namespace TD.GUI.Screens.MainMenu
{
    public class MainMenuScreen : BaseScreen
    {
        public override void Show()
        {
            gameObject.SetActive(true);
        }
        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

