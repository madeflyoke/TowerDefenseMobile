using UnityEngine;
using Zenject;
using TD.GamePlay.Managers;

namespace TD.GamePlay.Towers
{
    public abstract class BaseTower : MonoBehaviour
    {
        [Inject] private Pooler pooler;

        [SerializeField] protected float damage;
        [SerializeField] protected float attackRange;
        [SerializeField] protected float attackSpeed;
        [SerializeField] protected float cost;
        [SerializeField] protected float attackPower;
        public float AttackRange { get => attackRange; }
        public float AttackSpeed { get => attackSpeed; }
        public float AttackPower { get => attackPower; }
        public float Damage { get => damage; }
        public TowerAttacker Attacker { get; private set; }
        public TowerTargetter Targeter { get; private set; }

        private void Awake()
        {
            Attacker = GetComponentInChildren<TowerAttacker>();
            Targeter = GetComponentInChildren<TowerTargetter>();
            Attacker.Initialize(pooler);
            Targeter.Initialize(this, Attacker);
        }
        private void Start()
        {
            Targeter.isSearching = true;
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
        }
    }
}
