using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.Pieces
{
    /// <summary>
    /// 可移动地块
    /// </summary>
    public class MoveablePiece : MonoBehaviour
    {
        private GamePiece piece;
        private IEnumerator moveCoroutine;

        private void Awake()
        {
            piece = GetComponent<GamePiece>();
        }

        public void Move(int newX, int newY, float time)
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = MoveCoroutine(newX, newY, time);
            StartCoroutine(moveCoroutine);
        }

        /// <summary>
        /// 地块移动
        /// </summary>
        private IEnumerator MoveCoroutine(int newX, int newY, float time)
        {
            piece.X = newX;
            piece.Y = newY;

            var startPos = transform.position;
            var endPos = piece.GridRef.GetWorldPosition(newX, newY);

            for (float t = 0; t <= time; t += Time.deltaTime)
            {
                piece.transform.position = Vector3.Lerp(startPos, endPos, t / time);
                yield return 0;
            }
            piece.transform.position = endPos;
        }
    }
}
