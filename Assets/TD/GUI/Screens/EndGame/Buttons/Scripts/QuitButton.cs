using TD.GamePlay.Managers;
using TD.GUI.Buttons;
using Zenject;

namespace TD.GUI.Screens.EndGame.Buttons
{
    public class QuitButton : BaseButton
    {
        [Inject] private GameManager gameManager; 
        protected override void Listeners()
        {
            base.Listeners();
            gameManager.CheckButtonCall(this);
        }
    }
}

