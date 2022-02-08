using UnityEngine;
using PathCreation.Examples;
using Zenject;
using TD.GamePlay.Managers;
using System;

namespace TD.GamePlay.Units
{
    public abstract class BaseUnit : MonoBehaviour
    {
        [Inject] private GameManager gameManager;
        [Inject] private Pooler pooler;

        public event Action<BaseUnit> enemyDieEvent;

        [SerializeField] protected float speedMultiplier;
        [SerializeField] protected float maxHealthPoints;
        [SerializeField] protected float damage;
        [SerializeField] protected int killReward;
        [SerializeField] protected GameObject deathEffect;

        public float MovementSpeed { get => movementSpeed; }
        public float Damage { get => damage; }
        public PathFollower pathFollower { get; private set; }
        public float MaxHealthPoints { get => maxHealthPoints; }

        private float currenHealthPoints;
        private float movementSpeed;
        private HealthBar healthBar;
        private Animator animator;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            pathFollower = GetComponent<PathFollower>();
            pathFollower.speed *= speedMultiplier;
            movementSpeed = pathFollower.speed;
            healthBar = GetComponentInChildren<HealthBar>();
            healthBar.Initialize();
            healthBar.gameObject.SetActive(false);
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            gameManager.endGameEvent += StopUnit;
        }
        private void OnDisable()
        {
            ResetValues();
            gameManager.endGameEvent -= StopUnit;
        }

        public virtual void Move()        
        {
            pathFollower.canMove = true;
        }

        public virtual void GetDamage(float damage) 
        {      
            currenHealthPoints -= damage;
            if (currenHealthPoints<=0)
            {
                Die();
                return;
            }
            healthBar.gameObject.SetActive(true);
            healthBar.UpdateHealth(currenHealthPoints * 100 / maxHealthPoints);
        }

        protected virtual void Die()
        {
            gameManager.AddCurrency(killReward);
            pooler.GetObjectFromPool(deathEffect, transform.position);
            enemyDieEvent?.Invoke(this);
            gameObject.SetActive(false);
        }

        protected virtual void ResetValues()
        {
            pathFollower.ResetPath();
            currenHealthPoints = maxHealthPoints;
            healthBar.ResetHealthBar();
        }

        protected void StopUnit()
        {
            animator.enabled = false;
            pathFollower.canMove = false;
        }
    }
}

