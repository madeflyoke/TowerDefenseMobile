using UnityEngine;
using Zenject;
using TD.GamePlay.Managers;
using DG.Tweening;

namespace TD.GamePlay.Towers
{
    public abstract class BaseTower : MonoBehaviour
    {
        [Inject] private Pooler pooler;
        [Inject] private GameManager gameManager;

        [SerializeField] private BaseTower nextTowerLevel;
        [SerializeField] protected float damage;
        [SerializeField] protected float attackRange;
        [SerializeField] protected float attackSpeed;
        [SerializeField] protected int cost;
        [SerializeField] protected float attackPower;

        public int Cost { get => cost; }
        public float AttackRange { get => attackRange; }
        public float AttackSpeed { get => attackSpeed; }
        public float AttackPower { get => attackPower; }
        public float Damage { get => damage; }
        public TowerAttacker Attacker { get; private set; }
        public TowerTargetter Targeter { get; private set; }
        public BaseTower NextTowerLevel { get => nextTowerLevel; }

        private void Awake()
        {
            Attacker = GetComponentInChildren<TowerAttacker>();
            Targeter = GetComponentInChildren<TowerTargetter>();
            Targeter.Initialize(this, Attacker);
            Attacker.Initialize(pooler);
        }

        private void OnEnable()
        {
            gameManager.endGameEvent += StopTowerAttack;
            transform.DOPunchScale(Vector3.one * 0.5f, 0.2f);
        }
        private void OnDisable()
        {
            gameManager.endGameEvent -= StopTowerAttack;
        }

        private void StopTowerAttack()
        {
            Targeter.isSearching = false;
            Targeter.enabled = false;

            Attacker.isAttack = false;
            Attacker.shootingTween.Kill();
            Attacker.enabled = false;
        }

        private void Start()
        {
            Targeter.isSearching = true;
        }

        public void DestroyTower()
        {
            transform.DOScale(0f, 0.15f).OnComplete(() => Destroy(gameObject));
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
        }

    }
}
