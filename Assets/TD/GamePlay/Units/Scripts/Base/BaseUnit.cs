using UnityEngine;
using PathCreation.Examples;
using PathCreation;
using Zenject;
using TD.GamePlay.Managers;

namespace TD.GamePlay.Units
{
    public abstract class BaseUnit : MonoBehaviour
    {
        [Inject] private GameManager gameManager;

        [SerializeField] protected float speedMultiplier;
        [SerializeField] protected float maxHealthPoints;
        [SerializeField] protected float damage;
        [SerializeField] protected int killReward;
        public float MovementSpeed { get => movementSpeed; }
        public float Damage { get => damage; }
        public PathFollower pathFollower { get; private set; }
        public float MaxHealthPoints { get => maxHealthPoints; }

        private float currenHealthPoints;
        private float movementSpeed;
        private HealthBar healthBar;

        private void Awake()
        {
            pathFollower = GetComponent<PathFollower>();   
            pathFollower.pathCreator = FindObjectOfType<PathCreator>();    
            pathFollower.speed *= speedMultiplier;
            movementSpeed = pathFollower.speed;
            healthBar = GetComponentInChildren<HealthBar>();
            healthBar.Initialize();
            healthBar.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            ResetValues();
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
            gameObject.SetActive(false);
        }

        protected virtual void ResetValues()
        {
            pathFollower.ResetPath();
            currenHealthPoints = maxHealthPoints;
            healthBar.ResetHealthBar();
        }
    }
}

