using TD.GUI.Screens.GamePlay.BuildMenu.Buttons;

namespace TD.GUI.Screens.GamePlay.Menu.Buttons
{
    public class ResumeGamePlayButton : BaseButton
    {
        protected override void Listeners()
        {
            transform.parent.GetComponent<GamePlayMenu>().Hide();
        }
    }
}

