using UnityEngine;

namespace TD.GamePlay.Units
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Transform backGround;
        [SerializeField] private Transform fill;
        private Camera cam;

        public void Initialize()
        {
            cam = Camera.main;
            ResetHealthBar();
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + cam.transform.forward);
        }

        public void ResetHealthBar()
        {
            Vector3 scale = Vector3.one;
            scale.x = 1f;
            fill.localScale = scale;
            scale.x = 0f;
            backGround.localScale = scale;
            gameObject.SetActive(false);
        }

        public void UpdateHealth(float remainHealthPercent)
        {
            fill.localScale = new Vector3(remainHealthPercent / 100, 1f, 1f);
            backGround.localScale = new Vector3(1 - fill.localScale.x, 1f, 1f);
        }
    }
}

