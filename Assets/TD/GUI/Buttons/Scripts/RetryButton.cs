using TD.GamePlay.Managers;
using TD.GUI.Buttons;
using UnityEngine;
using Zenject;

namespace TD.GUI.Screens.GamePlay.HUD
{
    public class RetryButton : BaseButton
    {
        [Inject] private GameManager gameManager;
        
        protected override void Listeners()
        {
            base.Listeners();
            gameManager.CheckButtonCall(this);
        }
    }
}

