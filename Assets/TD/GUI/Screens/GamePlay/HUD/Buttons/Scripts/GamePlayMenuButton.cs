using TD.GUI.Screens.GamePlay.Menu;
using UnityEngine;
using DG.Tweening;
using TD.GUI.Buttons;

namespace TD.GUI.Screens.GamePlay.HUD.Buttons
{
    public class GamePlayMenuButton : BaseButton
    {
        [SerializeField] private GamePlayMenu gamePlayMenu; 
        protected override void Listeners()
        {
            base.Listeners();
            transform.DOPunchScale(Vector3.one * 0.2f, 0.1f);
            if (gamePlayMenu.gameObject.activeInHierarchy)
            {
                gamePlayMenu.Hide();
            }
            else
            {
                gamePlayMenu.Show();
            }            
        }
    }
}

