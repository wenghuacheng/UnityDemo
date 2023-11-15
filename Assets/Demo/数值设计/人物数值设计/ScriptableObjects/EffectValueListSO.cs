using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Design.Character.Stat
{
    [CreateAssetMenu(fileName = "EffectList", menuName = "人物数值/影响效果列表", order = 3)]
    public class EffectValueListSO : ScriptableObject
    {
        //效果
        public EffectValueSO[] effect;

        //描述
        public string description;
    }
}
