using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.SaveLoad
{
    /// <summary>
    /// �ýӿ��ò�ͬ�Ķ����ṩ���ݱ���
    /// </summary>
    public interface ISaveManager 
    {
        void LoadData(GameData data);

        void SaveData(ref GameData data);
    }
}
