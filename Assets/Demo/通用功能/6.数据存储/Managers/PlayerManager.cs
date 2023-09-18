using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.SaveLoad
{
    /// <summary>
    /// 测试类:模拟玩家数据的存储
    /// </summary>
    public class PlayerManager : MonoBehaviour, ISaveManager
    {
        //这里通过货币模拟玩家数据的存取
        public int Currency;


        public void LoadData(GameData data)
        {
            this.Currency = data.currency;
        }

        public void SaveData(ref GameData data)
        {
            data.currency = this.Currency;
        }

    }
}