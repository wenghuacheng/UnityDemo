using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.Games.JumpAJump
{
    /// <summary>
    /// �ϰ�����
    /// </summary>
    public class ObstacleDestoryer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private int socre;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Obstacle")
            {
                socre++;
                text.text = socre.ToString();
                Destroy(other.gameObject);
            }
        }
    }
}