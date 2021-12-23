using UnityEngine;
using System.Collections.Generic;
using System;

namespace TD.GamePlay.Managers
{
    public class Pooler : MonoBehaviour
    {

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
                    GameObject go = Instantiate(poolObject.gameObject,transform.position, Quaternion.identity, transform);
                    go.SetActive(false);
                    pool.Enqueue(go);
                }
                poolDict.Add(poolObject.gameObject, pool);

            }
        }

        public GameObject GetObjectFromPool(GameObject prefab, Vector3 pos)
        {
            if (poolDict.ContainsKey(prefab)==false)
            {
                throw new Exception(prefab + " is not key of dictionary");
            }
            var pool = poolDict[prefab];
            GameObject go = pool.Count>0&&!pool.Peek().activeInHierarchy?pool.Dequeue()
                                                                         :AddPoolObject(prefab, pool);

            go.transform.position = pos;
            go.SetActive(true);
            pool.Enqueue(go);
            return go;
        }

        private GameObject AddPoolObject(GameObject prefab, Queue<GameObject> pool)
        {
            GameObject go = Instantiate(prefab, transform.position, Quaternion.identity,transform);
            go.SetActive(false);
            return go;
        }
    }
}

