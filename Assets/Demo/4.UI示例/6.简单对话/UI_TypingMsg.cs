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
        //打字机效果脚本需要挂载到一个空物体上，因为需要系统执行update函数
        [SerializeField] private TypingTextWriter writer;

        [SerializeField] private Button button;

        //当前文本索引
        private int msgIndex;
        //当前输出文字脚本
        private TypingTextWriter.SingleTypingTextWriter curSingleTextWriter;

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                //模拟一个对话列表
                string[] msgList = new string[]
                    {
                "AAA BBB CCC",
                "DDD EEE FFF",
                "GGG HHH III",
                "JJJ KKK LLLL MMM"
                    };

                if (curSingleTextWriter != null && curSingleTextWriter.IsActive())
                {
                    //正在打印时点击直接显示全部
                    curSingleTextWriter.ShowAll();
                }
                else if (msgIndex < msgList.Length)
                {
                    //否则生成一个新的对话
                    string msg = msgList[msgIndex];
                    curSingleTextWriter = writer.AddWriter(TextCtrl, msg, 0.1f);
                    msgIndex++;
                }
            });
        }
    }
}