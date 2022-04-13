using UnityEngine;
using Zenject;
using TD.GamePlay.Managers;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace TD.GUI.Screens.EndGame
{
    public class EndGameScreen : BaseScreen
    {
        [Inject] private GameManager gameManager;

        [SerializeField] private float delayBeforeAppear;
        [SerializeField] private GameObject failTitle;
        [SerializeField] private GameObject successTitle;
        [SerializeField] private StarsAccrual starsAccrual;

        public async override void Show()
        {
            await UniTask.Delay((int)delayBeforeAppear * 1000);
            transform.DOScale(transform.localScale, 0.1f).OnStart(() => gameObject.SetActive(true))
                .From(Vector3.zero);
            gameObject.SetActive(true);
            if (starsAccrual.SetStars(gameManager.homeBase.currentDamageState))
            {
                successTitle.transform.DOScale(successTitle.transform.localScale,0.2f)
                    .From(successTitle.transform.localScale*1.3f)
                    .OnStart(()=>successTitle.SetActive(true))
                    .SetDelay(3f);
            }
            else
            {
                failTitle.transform.DOScale(failTitle.transform.localScale, 0.2f)
                    .From(failTitle.transform.localScale * 1.3f)
                    .OnStart(() => failTitle.SetActive(true))
                    .SetDelay(3f);
            }
        }

        public void ResetScreen()
        {
            starsAccrual.ResetStars();
            successTitle.SetActive(false);
            failTitle.SetActive(false);
        }

        public override void Hide()
        {
            ResetScreen();
            gameObject.SetActive(false);          
        }
    }
}

