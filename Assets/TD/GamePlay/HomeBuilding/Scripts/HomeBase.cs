using TD.GamePlay.Units;
using System;
using UnityEngine;

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

        private void GetDamage(float damage)
        {
            currentHealthPoints -= damage;
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

