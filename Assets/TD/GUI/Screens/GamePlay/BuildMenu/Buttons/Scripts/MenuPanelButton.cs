using TD.GUI.Buttons;

namespace TD.GUI.Screens.GamePlay.BuildMenu.Buttons
{
    public class MenuPanelButton : BaseButton
    {
        private BuildMenuController buildMenuController;

        protected override void Awake()
        {
            base.Awake();
            buildMenuController = GetComponentInParent<BuildMenuController>();  
        }

        protected override void Listeners()
        {
            base.Listeners();
           buildMenuController.HideMenu();
        }
    }
}

