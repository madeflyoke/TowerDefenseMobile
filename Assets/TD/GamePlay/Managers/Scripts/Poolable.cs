using UnityEngine;

namespace TD.GamePlay.Managers
{
    public class Poolable : MonoBehaviour
    {
        [SerializeField] private int count =10;
        public float Count { get => count; }

    }
}

