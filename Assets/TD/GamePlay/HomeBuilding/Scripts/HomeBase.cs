using TD.GamePlay.Units;
using System;
using UnityEngine;
using DG.Tweening;

namespace TD.GamePlay.HomeBuilding
{
    [RequireComponent(typeof(BoxCollider))]
    public class HomeBase : MonoBehaviour
    {
        private enum DamageState
        {
            None,
            First,
            Second
        }

        public event Action homeBaseDestroyedEvent;
        public event Action <float> homeBaseChangedHealthEvent;

        [SerializeField] private float maxHealthPoints;
        [SerializeField] private ParticleSystem damageEffect;
        [SerializeField] private ParticleSystem deathEffect;
        [SerializeField] private ParticleSystem firstStageDamageEffect;
        [SerializeField] private ParticleSystem secondStageDamageEffect;
        [Range(1, 100f)]
        [SerializeField] private int firstStageDamagePercentHP;
        [Range(1, 100f)]
        [SerializeField] private int secondStageDamagePercentHP;
        private float currentHealthPoints;
        private DamageState currentDamageState;
        public float MaxHealthPoints { get => maxHealthPoints; }

        private void Awake()
        {
            currentDamageState = DamageState.None;
            firstStageDamageEffect.gameObject.SetActive(false);
            secondStageDamageEffect.gameObject.SetActive(false);
            currentHealthPoints = maxHealthPoints;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                GetDamage(17);
            }
        }

        private void GetDamage(float damage)
        {    
            currentHealthPoints -= damage;
            homeBaseChangedHealthEvent?.Invoke(currentHealthPoints);
            damageEffect.Play();

            if (currentHealthPoints<=maxHealthPoints*firstStageDamagePercentHP/100
                &&currentDamageState==DamageState.None)
            {
                firstStageDamageEffect.gameObject.SetActive(true);
                firstStageDamageEffect.Play();
                currentDamageState = DamageState.First;
            }
            else if (currentHealthPoints<=maxHealthPoints*secondStageDamagePercentHP/100
                &&currentDamageState==DamageState.First)
            {
                secondStageDamageEffect.gameObject.SetActive(true);
                secondStageDamageEffect.Play();
                currentDamageState=DamageState.Second;
            }

            if (currentHealthPoints<=0)
            {
                deathEffect.Play();
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

