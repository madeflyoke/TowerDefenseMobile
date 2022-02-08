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
        [SerializeField] private Transform turret;
        [SerializeField] private float turretRotationSpeed;
        [SerializeField] private ParticleSystem muzzleFlashEffect;
        [SerializeField] private GameObject projectileExplosionEffect;
        public bool isAttack { get; set; }
        public BaseUnit currentTarget { get; private set; }
        public Tween shootingTween { get; private set; }

        private float prevAttackTime;
        private Pooler pooler;
        private ParticleSystem muzzleFlash;

        public void Initialize(Pooler pooler)
        {
            this.pooler = pooler;
            muzzleFlash = Instantiate(
                muzzleFlashEffect, attackPoint.position, muzzleFlashEffect.transform.rotation, attackPoint);
        }

        private void Shoot(float attackPower, float damage)
        {
            muzzleFlash.Play();
            GameObject currentProjectile = pooler.GetObjectFromPool(projectilePrefab, attackPoint.position);
            shootingTween = currentProjectile.transform.DOMove(CorrectShot(attackPower), 1 / attackPower).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    if (currentProjectile != null && pooler != null)
                    {
                        Collider[] colliders = Physics.OverlapSphere(currentProjectile.transform.position, splashHitRadius, 1 << 6);
                        foreach (Collider collider in colliders)
                        {
                            if (collider.gameObject.activeInHierarchy)
                            {
                                collider.gameObject.GetComponent<BaseUnit>().GetDamage(damage);
                            }
                        }
                        pooler.GetObjectFromPool(projectileExplosionEffect, currentProjectile.transform.position);
                        currentProjectile.SetActive(false);
                    }
                }).OnKill(() =>
                {
                    currentProjectile.SetActive(false);

                });
        }

        private void OnDisable()
        {
            shootingTween.Kill();
        }

        private Vector3 CorrectShot(float attackPower)
        {
            Vector3 correctPosition = currentTarget.pathFollower.GetPointOnPath(1 / attackPower * currentTarget.MovementSpeed);
            return correctPosition;
        }

        public async void Attack(BaseUnit enemy, float attackSpeed, float attackPower, float damage)
        {
            currentTarget = enemy;
            while (isAttack)
            {
                RotateTurret();
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

        private void RotateTurret()
        {
            Quaternion newRot = Quaternion.LookRotation(currentTarget.transform.position - turret.position, Vector3.up);
            newRot.x = 0f;
            newRot.z = 0f;
            turret.rotation = Quaternion.Slerp(turret.rotation, newRot, turretRotationSpeed * Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            if (currentTarget != null)
            {
                Gizmos.DrawRay(attackPoint.position, (currentTarget.transform.position - attackPoint.position));
            }
        }

        private void OnDestroy()
        {
            isAttack = false;
        }
    }
}

