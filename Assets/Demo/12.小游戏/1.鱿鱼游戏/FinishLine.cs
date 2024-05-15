using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.SquidGame
{
    /// <summary>
    /// �յ����ж�
    /// </summary>
    public class FinishLine : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D collision)
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("���Ӯ��");
                player.Win();
            }

            var computerPlayer = collision.gameObject.GetComponent<ComputerPlayer>();
            if (computerPlayer != null)
            {
                Debug.Log("�������Ӯ��");
                computerPlayer.Win();
            }
        }
    }
}