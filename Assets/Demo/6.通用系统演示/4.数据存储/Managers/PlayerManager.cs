using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Common.SaveLoad
{
    /// <summary>
    /// 测试类:模拟玩家数据的存储
    /// </summary>
    public class PlayerManager : MonoBehaviour, ISaveManager
    {
        [SerializeField] private TextMeshProUGUI txt;

        //这里通过货币模拟玩家数据的存取
        public int Currency;


        public void LoadData(GameData data)
        {
            this.Currency = data.currency;
            txt.text = this.Currency.ToString();
        }

        public void SaveData(ref GameData data)
        {
            data.currency = this.Currency;
        }


        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 400, 100, 100), "Add"))
            {
                this.Currency++;
                txt.text = this.Currency.ToString();
            }
        }
    }
}