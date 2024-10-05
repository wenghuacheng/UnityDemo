using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.Pieces
{
    public class ClearablePiece : MonoBehaviour
    {
        private GamePiece piece;

        public bool isBeingCleared;

        private void Awake()
        {
            piece = GetComponent<GamePiece>();
        }

        public void Clear()
        {
            isBeingCleared = true;
            StartCoroutine(ClearCoroutine());
        }

        private IEnumerator ClearCoroutine()
        {
            yield return new WaitForSeconds(0.2f);
            Destroy(this.gameObject);
        }
    }
}