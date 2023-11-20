using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.SaveLoad
{
    /// <summary>
    /// 该接口让不同的对象提供数据保存
    /// </summary>
    public interface ISaveManager 
    {
        void LoadData(GameData data);

        void SaveData(ref GameData data);
    }
}
