using TD.GamePlay.Towers.Projectiles;
using TD.GamePlay.Managers;
using TD.GamePlay.Units;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Zenject;

namespace TD.GamePlay.Towers
{
    public class TowerAttacker : MonoBehaviour
    {
        [Inject] private Pooler pooler;

        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Transform attackPoint;

        public bool isAttack { get; set; }
        public BaseUnit currentTarget { get; private set; }
        private float prevAttackTime;

        private void Shoot(float attackPower, float damage)
        {
            GameObject proj = pooler.GetObjectFromPool(projectilePrefab.gameObject, attackPoint.position);
            proj.transform.DOMove(CorrectShot(attackPower), 1 / attackPower).SetEase(Ease.Linear)
                .OnComplete(() => { proj.SetActive(false);
                    if (currentTarget != null) currentTarget.GetDamage(damage);});
        }

        private Vector3 CorrectShot(float attackPower)
        {
            Vector3 additionalPos = currentTarget.transform.forward * (currentTarget.MovementSpeed * (1f / attackPower));
            Vector3 correctPos = currentTarget.transform.position + additionalPos;            
            return correctPos;
        }

        public async void Attack(BaseUnit enemy, float attackSpeed, float attackPower, float damage)
        {
            currentTarget = enemy;
            while (isAttack)
            {
                if (currentTarget == null)
                {
                    return;
                }
                else if(Time.time > prevAttackTime + (1 / attackSpeed))
                {
                    Shoot(attackPower, damage);
                    prevAttackTime = Time.time;
                }
                await UniTask.Yield();
            }
            currentTarget = null;
        }
    }
}

