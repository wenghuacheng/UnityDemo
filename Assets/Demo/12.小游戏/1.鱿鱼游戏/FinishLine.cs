using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.SquidGame
{
    /// <summary>
    /// 终点线判定
    /// </summary>
    public class FinishLine : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D collision)
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("玩家赢了");
                player.Win();
            }

            var computerPlayer = collision.gameObject.GetComponent<ComputerPlayer>();
            if (computerPlayer != null)
            {
                Debug.Log("电脑玩家赢了");
                computerPlayer.Win();
            }
        }
    }
}