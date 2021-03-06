using UnityEngine;
using System.Collections.Generic;
using System;
using Zenject;

namespace TD.GamePlay.Managers
{
    public class Pooler : MonoBehaviour
    {
        [Inject] private DiContainer diContainer;

        [SerializeField] private List<Poolable> poolObjects;
        private Dictionary<GameObject, Queue<GameObject>> poolDict;
               
        private void Awake()
        {
            poolDict = new Dictionary<GameObject, Queue<GameObject>>();
            Spawn();
        }

        private void Spawn()
        {
            foreach (Poolable poolObject in poolObjects)
            {
                Queue<GameObject> pool = new Queue<GameObject>();
                for (int i = 0; i < poolObject.Count; i++)
                {
                    GameObject go = diContainer.InstantiatePrefab(poolObject.gameObject, transform.position, Quaternion.identity, transform);
                    go.SetActive(false);
                    pool.Enqueue(go);
                }
                poolDict.Add(poolObject.gameObject, pool);

            }
        }

        public GameObject GetObjectFromPool(GameObject prefab, Vector3 pos)
        {
            if (poolDict.ContainsKey(prefab) == false)
            {
                throw new Exception(prefab + " is not key of dictionary");
            }
            var pool = poolDict[prefab];
            GameObject poolableObject = pool.Count > 0 && !pool.Peek().activeInHierarchy ? pool.Dequeue()
                                                                         : AddPoolObject(prefab);

            poolableObject.transform.position = pos;
            poolableObject.SetActive(true);
            pool.Enqueue(poolableObject);
            return poolableObject;
        }

        private GameObject AddPoolObject(GameObject prefab)
        {
            GameObject go = diContainer.InstantiatePrefab(prefab.gameObject, transform.position, Quaternion.identity, transform);
            go.SetActive(false);
            return go;
        }
    }
}

