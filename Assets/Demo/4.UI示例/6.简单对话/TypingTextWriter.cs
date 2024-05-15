using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.UI
{
    public class TypingTextWriter : MonoBehaviour
    {
        private List<SingleTypingTextWriter> textList;

        private void Awake()
        {
            textList = new List<SingleTypingTextWriter>();
        }

        public SingleTypingTextWriter AddWriter(TextMeshProUGUI textCtrl, string content, float characterIntervalTime)
        {
            SingleTypingTextWriter newItem = new SingleTypingTextWriter(textCtrl, content, characterIntervalTime);
            textList.Add(newItem);
            return newItem;
        }

        private void Update()
        {
            Debug.Log(textList.Count);
            List<SingleTypingTextWriter> removeList = new List<SingleTypingTextWriter>();
            //更新所有当前对话
            foreach (SingleTypingTextWriter item in textList)
            {
                if (!item.Update())
                {
                    removeList.Add(item);
                }
            }

            //移除已经结束的对话
            removeList.ForEach(item => { textList.Remove(item); });
        }


        /// <summary>
        /// 单个对话
        /// </summary>
        public class SingleTypingTextWriter
        {
            private TextMeshProUGUI textCtrl;
            private string content;
            private float time;
            //每个字符的显示间隔时间
            private float characterIntervalTime;
            //当前显示字符索引
            private int curCharacterIndex;

            public SingleTypingTextWriter(TextMeshProUGUI textCtrl, string content, float characterIntervalTime)
            {
                this.textCtrl = textCtrl;
                this.content = content;
                this.characterIntervalTime = characterIntervalTime;
                this.curCharacterIndex = 0;
            }

            public bool Update()
            {
                if (textCtrl == null) return false;

                //通过每隔一定时间间隔输出一个字符来实现打字机效果
                time -= Time.deltaTime;
                //使用while可以防止低帧率下总输出时间变长的问题
                while (time <= 0 && IsActive())
                {
                    curCharacterIndex++;
                    time = characterIntervalTime;
                    //通过截取的方式显示当前要显示的字符
                    textCtrl.text = content.Substring(0, curCharacterIndex);
                    //通过透明文字将固定字符输出位置
                    //如：Hello输出H时，ello作为透明文字进行占位
                    textCtrl.text += $"<color=#00000000>{content.Substring(curCharacterIndex)}</color>";
                }

                if (curCharacterIndex >= content.Length)
                    return false;

                return true;
            }

            /// <summary>
            /// 是否正在打印
            /// </summary>
            /// <returns></returns>
            public bool IsActive()
            {
                return curCharacterIndex < content.Length;
            }

            /// <summary>
            /// 显示全部文字
            /// </summary>
            public void ShowAll()
            {
                curCharacterIndex = content.Length;
                textCtrl.text = content;
            }
        }
    }
}