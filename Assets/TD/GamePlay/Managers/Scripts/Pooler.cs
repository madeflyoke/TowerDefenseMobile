using UnityEngine;
using System.Collections.Generic;
using System;

namespace TD.GamePlay.Managers
{
    public class Pooler : MonoBehaviour
    {
        [Serializable]
        public struct PoolObject
        {
            public int count;
            public GameObject prefabKey;
        }

        [SerializeField] private List<PoolObject> poolObjects;
        private Dictionary<GameObject, Queue<GameObject>> poolDict;
       
        private void Awake()
        {
            poolDict = new Dictionary<GameObject, Queue<GameObject>>();
            Spawn();
        }

        private void Spawn()
        {
            foreach (PoolObject poolObject in poolObjects)
            {
                Queue<GameObject> pool = new Queue<GameObject>();
                for (int i = 0; i < poolObject.count; i++)
                {                 
                    GameObject go = Instantiate(poolObject.prefabKey,transform.position, Quaternion.identity, transform);
                    go.SetActive(false);
                    pool.Enqueue(go);
                }
                poolDict.Add(poolObject.prefabKey, pool);
            }
        }

        public GameObject GetObjectFromPool(GameObject prefabKey, Vector3 pos)
        {
            if (poolDict.ContainsKey(prefabKey)==false)
            {
                throw new Exception(prefabKey + " is not key of dictionary");
            }
            var pool = poolDict[prefabKey];
            GameObject go = pool.Dequeue();
            go.transform.position = pos;
            go.SetActive(true);
            pool.Enqueue(go);
            return go;
        }
    }
}

