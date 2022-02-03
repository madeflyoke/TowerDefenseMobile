using TD.GamePlay.Managers;
using TD.GUI.Screens.GamePlay.BuildMenu.Buttons;
using DG.Tweening;
using UnityEngine;

namespace TD.GUI.Screens.GamePlay.HUD.Buttons
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
            transform.DOPunchScale(Vector3.one * 0.2f, 0.1f)
                .OnComplete(() => gameObject.SetActive(false));        
        }
    }
}

