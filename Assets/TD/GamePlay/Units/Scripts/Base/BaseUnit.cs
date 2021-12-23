using UnityEngine;
using PathCreation.Examples;
using PathCreation;

namespace TD.GamePlay.Units
{
    public class BaseUnit : MonoBehaviour
    {
        [SerializeField] protected float speedMultiplier;
        [SerializeField] protected float maxHealthPoints;
        [SerializeField] protected float damage;
        public float MovementSpeed { get => movementSpeed; }
        public float Damage { get => damage; }

        private float currenHealthPoints;
        private float movementSpeed;
        public PathFollower pathFollower { get; private set; }

        private void Awake()
        {
            pathFollower = GetComponent<PathFollower>();   
            pathFollower.pathCreator = FindObjectOfType<PathCreator>();    
            pathFollower.speed *= speedMultiplier;
            movementSpeed = pathFollower.speed;
        }

        private void OnEnable()
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
            }
        }

        protected virtual void Die()
        {
            gameObject.SetActive(false);
        }

        protected virtual void ResetValues()
        {
            pathFollower.ResetPath();
            currenHealthPoints = maxHealthPoints;
        }
    }
}

