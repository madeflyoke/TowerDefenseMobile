using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TD.GUI.Screens.GamePlay.Menu
{
    public class GamePlayMenu : MonoBehaviour
    {
        public void Show()
        {
            transform.DOScale(transform.localScale, 0.1f).OnStart(() => gameObject.SetActive(true))
                 .From(Vector3.zero);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

    }

}
