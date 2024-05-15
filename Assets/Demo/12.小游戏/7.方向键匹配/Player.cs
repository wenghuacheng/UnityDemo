using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.MatchDirection
{
    public class Player : MonoBehaviour
    {
        private float keyTime;
        private float keyInterval = 0.1f;

        //玩家按键记录序列
        private List<KeyCode> playerKeyStore = new List<KeyCode>();

        void Update()
        {
            CheckPlayerKeyStore();
            KeyDownHandler();
            MatchKey(playerKeyStore);
        }

        #region 按键处理
        /// <summary>
        /// 检查按键序列
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
                //到时间清理按键
                playerKeyStore.Clear();
            }
        }

        /// <summary>
        /// 玩家按键
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
        /// 按钮检查
        /// </summary>
        private void MatchKey(List<KeyCode> keyList)
        {
            if (keyList.Count <= 0) return;

            var bl = SequenceManager.Instance.MatchKey(keyList);
            if (bl)
            {
                //匹配成功，清空
                keyTime = 0;
                playerKeyStore.Clear();
            }
        }
        #endregion
    }
}