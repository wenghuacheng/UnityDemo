using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.Pieces
{
    /// <summary>
    /// �ؿ����
    /// </summary>
    [Serializable]
    public struct PiecePrefab
    {
        public PieceType type;
        public GameObject prefab;
    }
}