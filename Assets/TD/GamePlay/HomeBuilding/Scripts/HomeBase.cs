using TD.GamePlay.Units;
using System;
using UnityEngine;
using DG.Tweening;

namespace TD.GamePlay.HomeBuilding
{
    [RequireComponent(typeof(BoxCollider))]
    public class HomeBase : MonoBehaviour
    {
        public event Action homeBaseDestroyedEvent;

        [SerializeField] private float maxHealthPoints;
        private float currentHealthPoints;

        private void Awake()
        {
            currentHealthPoints = maxHealthPoints;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                GetDamage(1);
            }
        }
        private void GetDamage(float damage)
        {    
            currentHealthPoints -= damage;
            transform.DOShakePosition(0.3f,0.2f);
            if (currentHealthPoints<=0)
            {
                homeBaseDestroyedEvent?.Invoke();
                return;
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 6) //enemy
            {
                BaseUnit enemy = other.gameObject.GetComponent<BaseUnit>();
                GetDamage(enemy.Damage);
                enemy.gameObject.SetActive(false);
            }
        }
    }
}

