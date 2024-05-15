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
            //�������е�ǰ�Ի�
            foreach (SingleTypingTextWriter item in textList)
            {
                if (!item.Update())
                {
                    removeList.Add(item);
                }
            }

            //�Ƴ��Ѿ������ĶԻ�
            removeList.ForEach(item => { textList.Remove(item); });
        }


        /// <summary>
        /// �����Ի�
        /// </summary>
        public class SingleTypingTextWriter
        {
            private TextMeshProUGUI textCtrl;
            private string content;
            private float time;
            //ÿ���ַ�����ʾ���ʱ��
            private float characterIntervalTime;
            //��ǰ��ʾ�ַ�����
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

                //ͨ��ÿ��һ��ʱ�������һ���ַ���ʵ�ִ��ֻ�Ч��
                time -= Time.deltaTime;
                //ʹ��while���Է�ֹ��֡���������ʱ��䳤������
                while (time <= 0 && IsActive())
                {
                    curCharacterIndex++;
                    time = characterIntervalTime;
                    //ͨ����ȡ�ķ�ʽ��ʾ��ǰҪ��ʾ���ַ�
                    textCtrl.text = content.Substring(0, curCharacterIndex);
                    //ͨ��͸�����ֽ��̶��ַ����λ��
                    //�磺Hello���Hʱ��ello��Ϊ͸�����ֽ���ռλ
                    textCtrl.text += $"<color=#00000000>{content.Substring(curCharacterIndex)}</color>";
                }

                if (curCharacterIndex >= content.Length)
                    return false;

                return true;
            }

            /// <summary>
            /// �Ƿ����ڴ�ӡ
            /// </summary>
            /// <returns></returns>
            public bool IsActive()
            {
                return curCharacterIndex < content.Length;
            }

            /// <summary>
            /// ��ʾȫ������
            /// </summary>
            public void ShowAll()
            {
                curCharacterIndex = content.Length;
                textCtrl.text = content;
            }
        }
    }
}