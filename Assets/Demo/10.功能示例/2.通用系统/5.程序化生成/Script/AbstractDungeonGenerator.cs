using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.PCG
{
    public abstract class AbstractDungeonGenerator : MonoBehaviour
    {
        [SerializeField] protected TilemapVisualizer tilemapVisualizer;
        [SerializeField] protected Vector2Int startPostion = Vector2Int.zero;

        public void GenerateGungeon()
        {
            tilemapVisualizer.Clear();
            RunProceduralGeneration();
        }

        protected abstract void RunProceduralGeneration();
    }
}