using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.GamePlay.Grid
{
    public partial class GridCreator : MonoBehaviour
    {
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private float cellsStep;
        [SerializeField] private int gridSize;
        [SerializeField] private List<Cell> cells = new List<Cell>();

        private void Awake()
        {
            //cells = new List<Cell>();   
        }

        public void CreateGrid()
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    Vector3 pos = new Vector3(
                        startPosition.x + x * cellsStep, startPosition.y, startPosition.z + z * cellsStep);
                    cells.Add(new Cell() { Position = pos });
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (cells.Count>gridSize)
            {
                startPosition = transform.position;
                foreach (var item in cells)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(item.Position, 0.5f);
                }
                return;
            }
            Debug.Log("no");
            CreateGrid();         
        }
    }
}

