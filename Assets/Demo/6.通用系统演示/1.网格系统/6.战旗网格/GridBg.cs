using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.Common.Grids.BattleFlagDemo
{
    public class GridBg : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private Vector2 pos = Vector2.zero;

        public void SetIndex(Vector2 pos)
        {
            this.pos = pos;
            SetText(this.pos);
        }

        //设置用于显示的坐标
        public void SetText(Vector2 content)
        {
            text.text = content.ToString();
        }
    }
}