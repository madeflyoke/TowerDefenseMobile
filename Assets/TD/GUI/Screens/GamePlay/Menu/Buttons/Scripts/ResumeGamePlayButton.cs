using TD.GUI.Buttons;

namespace TD.GUI.Screens.GamePlay.Menu.Buttons
{
    public class ResumeGamePlayButton : BaseButton
    {
        protected override void Listeners()
        {
            base.Listeners();
            transform.parent.GetComponent<GamePlayMenu>().Hide();
        }
    }
}

