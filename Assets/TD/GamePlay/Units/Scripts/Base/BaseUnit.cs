using UnityEngine;
using PathCreation.Examples;

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
        private PathFollower pathFollower;

        private void Awake()
        {
            pathFollower = GetComponent<PathFollower>();    
            pathFollower.speed *= speedMultiplier;
            movementSpeed = pathFollower.speed;
            currenHealthPoints = maxHealthPoints;
        }

        protected virtual void Move() { }
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
            gameObject.layer = 7;
            Destroy(gameObject);
        }
    }
}

