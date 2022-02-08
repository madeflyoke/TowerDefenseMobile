using TD.GUI.Buttons;
using UnityEngine;

namespace TD.GUI.Screens.MainMenu.Buttons
{
    public class ExitButton : BaseButton
    {
        protected override void Listeners()
        {
            Application.Quit();
        }
    }
}

