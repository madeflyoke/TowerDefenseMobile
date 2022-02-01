using TD.GamePlay.Managers;
using TD.GUI.Screens.GamePlay.BuildMenu.Buttons;
using Zenject;

namespace TD.GUI.Screens.GamePlay.HUD
{
    public class RetryButton : BaseButton
    {
        [Inject] private GameManager gameManager;
        protected override void Listeners()
        {
            gameManager.ResetLevel();
        }
    }
}

