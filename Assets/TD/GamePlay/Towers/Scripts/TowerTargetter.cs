using System.Collections.Generic;
using System.Linq;
using TD.GamePlay.Units;
using UnityEngine;

namespace TD.GamePlay.Towers
{
    [RequireComponent(typeof(SphereCollider))]
    public class TowerTargetter : MonoBehaviour
    {
        [SerializeField] private GameObject attackRangeCircle;
        public bool isSearching { get; set; }
        private SphereCollider attackRangeCollider;
        private BaseTower baseTower;
        private List<Collider> colliders;
        private TowerAttacker attacker;
        public GameObject AttackRangeCircle { get=>attackRangeCircle;}
        public void Initialize(BaseTower baseTower, TowerAttacker towerAttacker)
        {
            this.baseTower = baseTower;
            this.attacker = towerAttacker;
            colliders = new List<Collider>();
            attackRangeCollider = gameObject.GetComponent<SphereCollider>();
            attackRangeCollider.isTrigger = true;
            attackRangeCollider.radius = baseTower.AttackRange;
            attackRangeCircle.transform.localScale *= baseTower.AttackRange / 5.1f; //circle sprite size coef 
            attackRangeCircle.gameObject.SetActive(false);
        }

        private void Update() //if fps drop then Fixed!!!!!!
        {
            if (isSearching&&(attacker.currentTarget==null/*||!attacker.currentTarget.gameObject.activeInHierarchy)*/))
            {
                colliders = Physics.OverlapSphere(transform.position, baseTower.AttackRange, layerMask: 1 << 6).ToList();

                if (colliders.Count() != 0)
                {
                    SetTarget();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 6)
            {
                RemoveTarget(other);
            }
        }

        private void RemoveTarget(Collider target)
        {
            foreach (var item in colliders.ToList())
            {
                if (item == target)
                {
                    colliders.Remove(item);
                    if (item.gameObject == attacker.currentTarget.gameObject)
                    {
                        attacker.isAttack = false;
                    }
                }
            }
        }

        private void SetTarget()
        {
            BaseUnit enemy = FindClosestTarget();
            if (enemy != null)
            {
                attacker.isAttack = true;
                attacker.Attack(enemy, baseTower.AttackSpeed, baseTower.AttackPower, baseTower.Damage);
            }
            else
            {
                throw new System.NullReferenceException(enemy + " not exist");
            }
        }

        private BaseUnit FindClosestTarget()
        {
            Collider closest = null;
            float closestDistance = 0;
            for (int i = 0; i < colliders.Count; i++)
            {
                float distance = (colliders[i].transform.position - transform.position).magnitude;
                if (closestDistance == 0)
                {
                    closestDistance = distance;
                    closest = colliders[i];
                }
                else
                    closest = distance < closestDistance ? colliders[i] : closest;
            }
            return closest.GetComponent<BaseUnit>();
        }
    }
}

