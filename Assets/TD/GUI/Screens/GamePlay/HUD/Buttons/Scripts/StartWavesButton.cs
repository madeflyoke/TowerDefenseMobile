using TD.GamePlay.Managers;
using TD.GUI.Screens.GamePlay.BuildMenu.Buttons;

namespace TD.GUI.Screens.GamePlay.HUD
{
    public class StartWavesButton : BaseButton
    {
        private WavesSpawner wavesSpawner;
        protected override void Awake()
        {
            base.Awake();
            wavesSpawner = FindObjectOfType<WavesSpawner>();
        }
        protected override void Listeners()
        {
            wavesSpawner.StartWaves();
            gameObject.SetActive(false);
        }
    }
}

