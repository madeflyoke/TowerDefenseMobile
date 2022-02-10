using TD.GamePlay.Managers;
using TD.GUI.Buttons;
using Zenject;
using DG.Tweening;
using UnityEngine;

namespace TD.GUI.Screens.MainMenu.Buttons
{
    public class StartGameButton : BaseButton
    {
        [Inject] private GameManager gameManager;
        protected override void Listeners()
        {
            base.Listeners();
            transform.DOPunchScale(Vector3.one * 0.1f, 0.2f).OnComplete(() => gameManager.CheckButtonCall(this));
            base.OnDisable();
        }
    }
}

