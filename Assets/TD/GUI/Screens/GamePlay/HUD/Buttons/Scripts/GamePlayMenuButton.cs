using TD.GUI.Screens.GamePlay.Menu;
using UnityEngine;
using DG.Tweening;
using TD.GUI.Buttons;
using Zenject;
using TD.Services.Firebase;

namespace TD.GUI.Screens.GamePlay.HUD.Buttons
{
    public class GamePlayMenuButton : BaseButton
    {
        [Inject] private AnalyticsManager analyticsManager;

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
                analyticsManager.SendEvent(LogEventName.SettingsShowHideEvent,
                    new EventParameter(LogEventParameterName.ShowHideBoolean, true));
                gamePlayMenu.Show();           
            }            
        }
    }
}

