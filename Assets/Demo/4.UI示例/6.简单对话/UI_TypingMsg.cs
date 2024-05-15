using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class UI_TypingMsg : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI TextCtrl;
        //���ֻ�Ч���ű���Ҫ���ص�һ���������ϣ���Ϊ��Ҫϵͳִ��update����
        [SerializeField] private TypingTextWriter writer;

        [SerializeField] private Button button;

        //��ǰ�ı�����
        private int msgIndex;
        //��ǰ������ֽű�
        private TypingTextWriter.SingleTypingTextWriter curSingleTextWriter;

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                //ģ��һ���Ի��б�
                string[] msgList = new string[]
                    {
                "AAA BBB CCC",
                "DDD EEE FFF",
                "GGG HHH III",
                "JJJ KKK LLLL MMM"
                    };

                if (curSingleTextWriter != null && curSingleTextWriter.IsActive())
                {
                    //���ڴ�ӡʱ���ֱ����ʾȫ��
                    curSingleTextWriter.ShowAll();
                }
                else if (msgIndex < msgList.Length)
                {
                    //��������һ���µĶԻ�
                    string msg = msgList[msgIndex];
                    curSingleTextWriter = writer.AddWriter(TextCtrl, msg, 0.1f);
                    msgIndex++;
                }
            });
        }
    }
}