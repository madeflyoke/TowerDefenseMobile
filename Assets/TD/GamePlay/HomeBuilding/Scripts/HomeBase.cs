using TD.GamePlay.Units;
using System;
using UnityEngine;
using DG.Tweening;
using TD.GamePlay.Managers;
using Zenject;

namespace TD.GamePlay.HomeBuilding
{
    [RequireComponent(typeof(BoxCollider))]
    public class HomeBase : MonoBehaviour
    {
        public enum DamageState
        {
            None,
            Damaged,
            First,
            Second,
            Destroyed
        }

        public event Action homeBaseDestroyedEvent;
        public event Action <float> homeBaseChangedHealthEvent;

        [SerializeField] private float maxHealthPoints;
        [SerializeField] private ParticleSystem damageEffect;
        [SerializeField] private ParticleSystem deathEffect;
        [SerializeField] private float deathEffectDelay;
        [SerializeField] private ParticleSystem firstStageDamageEffect;
        [SerializeField] private ParticleSystem secondStageDamageEffect;
        [Range(1, 100f)]
        [SerializeField] private int firstStageDamagePercentHP;
        [Range(1, 100f)]
        [SerializeField] private int secondStageDamagePercentHP;
        private float currentHealthPoints;
        public DamageState currentDamageState { get;private set; }
        public float MaxHealthPoints { get => maxHealthPoints; }

        private void Awake()
        {
            DeathEffectDelayInitialize();
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
            SetDamageState();
            if (currentHealthPoints<=0)
            {
                deathEffect.Play();
                homeBaseDestroyedEvent?.Invoke();
                currentDamageState = DamageState.Destroyed;
                return;
            }

        }

        private void SetDamageState()
        {
            if (currentHealthPoints!=maxHealthPoints&&currentDamageState==DamageState.None)
            {
                currentDamageState = DamageState.Damaged;
            }
            else if (currentHealthPoints <= maxHealthPoints * firstStageDamagePercentHP / 100
                && currentDamageState == DamageState.Damaged)
            {
                firstStageDamageEffect.gameObject.SetActive(true);
                firstStageDamageEffect.Play();
                currentDamageState = DamageState.First;
            }
            else if (currentHealthPoints <= maxHealthPoints * secondStageDamagePercentHP / 100
                && currentDamageState == DamageState.First)
            {
                secondStageDamageEffect.gameObject.SetActive(true);
                secondStageDamageEffect.Play();
                currentDamageState = DamageState.Second;
            }
        }

        private void DeathEffectDelayInitialize()
        {
            var childParticles = deathEffect.gameObject.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem item in childParticles)
            {
                var settings = item.main;
                settings.startDelay = deathEffectDelay * settings.simulationSpeed;
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

