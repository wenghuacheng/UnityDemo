using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.MatchDirection
{
    public class Player : MonoBehaviour
    {
        private float keyTime;
        private float keyInterval = 0.1f;

        //��Ұ�����¼����
        private List<KeyCode> playerKeyStore = new List<KeyCode>();

        void Update()
        {
            CheckPlayerKeyStore();
            KeyDownHandler();
            MatchKey(playerKeyStore);
        }

        #region ��������
        /// <summary>
        /// ��鰴������
        /// </summary>
        private void CheckPlayerKeyStore()
        {
            keyTime -= Time.deltaTime;
            if (Input.anyKeyDown && playerKeyStore.Count <= 0)
            {
                keyTime = keyInterval;
            }

            if (keyTime <= 0 && playerKeyStore.Count > 0)
            {
                //��ʱ��������
                playerKeyStore.Clear();
            }
        }

        /// <summary>
        /// ��Ұ���
        /// </summary>
        private void KeyDownHandler()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                playerKeyStore.Add(KeyCode.LeftArrow);
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                playerKeyStore.Add(KeyCode.UpArrow);
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                playerKeyStore.Add(KeyCode.RightArrow);
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                playerKeyStore.Add(KeyCode.DownArrow);
        }

        /// <summary>
        /// ��ť���
        /// </summary>
        private void MatchKey(List<KeyCode> keyList)
        {
            if (keyList.Count <= 0) return;

            var bl = SequenceManager.Instance.MatchKey(keyList);
            if (bl)
            {
                //ƥ��ɹ������
                keyTime = 0;
                playerKeyStore.Clear();
            }
        }
        #endregion
    }
}