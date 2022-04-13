using TD.GamePlay.Managers;
using DG.Tweening;
using UnityEngine;
using TD.GUI.Buttons;

namespace TD.GUI.Screens.GamePlay.HUD.Buttons
{
    public class StartWavesButton : BaseButton
    {
        private WavesSpawner wavesSpawner;

        protected override void OnEnable()
        {
            base.OnEnable();
            wavesSpawner = FindObjectOfType<WavesSpawner>();
        }

        protected override void Listeners()
        {
            base.Listeners();
            wavesSpawner.StartWaves();
            transform.DOPunchScale(Vector3.one * 0.2f, 0.1f)
                .OnComplete(() => gameObject.SetActive(false));        
        }
    }
}

