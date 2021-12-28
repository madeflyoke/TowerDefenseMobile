using TD.GamePlay.Managers;
using TD.GamePlay.Units;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace TD.GamePlay.Towers
{
    public class TowerAttacker : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float splashHitRadius;
        public bool isAttack { get; set; }
        public BaseUnit currentTarget { get; private set; }

        private float prevAttackTime;
        private Pooler pooler;

        public void Initialize(Pooler pooler)
        {
            this.pooler = pooler;
        }

        private void Shoot(float attackPower, float damage)
        {
            GameObject proj = pooler.GetObjectFromPool(projectilePrefab, attackPoint.position);
            proj.transform.DOMove(CorrectShot(attackPower), 1 / attackPower).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Collider[] colliders = Physics.OverlapSphere(proj.transform.position, splashHitRadius, 1 << 6);
                    foreach (Collider collider in colliders)
                    {
                        if (collider.gameObject.activeInHierarchy)
                        {
                            collider.gameObject.GetComponent<BaseUnit>().GetDamage(damage);
                        }
                    }    
                    proj.SetActive(false);
                    //if (currentTarget != null) currentTarget.GetDamage(damage);
                });
        }

        private Vector3 CorrectShot(float attackPower)
        {
            Vector3 correctPosition = currentTarget.pathFollower.GetPointOnPath(1 / attackPower * currentTarget.MovementSpeed);
            //Vector3 additionalPos = currentTarget.transform.forward * (currentTarget.MovementSpeed * (1 / attackPower));
            //Vector3 correctPos = currentTarget.transform.position + additionalPos;            
            return correctPosition;
        }

        public async void Attack(BaseUnit enemy, float attackSpeed, float attackPower, float damage)
        {
            currentTarget = enemy;
            while (isAttack)
            {
                if (currentTarget.gameObject.activeInHierarchy == false)
                {
                    isAttack = false;
                    break;
                }
                else if (Time.time > prevAttackTime + (1 / attackSpeed))
                {
                    Shoot(attackPower, damage);
                    prevAttackTime = Time.time;
                }
                await UniTask.Yield();
            }
            currentTarget = null;
        }

        private void OnDrawGizmos()
        {
            if (currentTarget != null)
            {
                Gizmos.DrawRay(attackPoint.position, (currentTarget.transform.position - attackPoint.position));
            }
        }
    }
}

