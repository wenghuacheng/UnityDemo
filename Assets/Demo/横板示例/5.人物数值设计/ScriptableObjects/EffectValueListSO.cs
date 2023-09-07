using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Stat
{
    [CreateAssetMenu(fileName = "EffectList", menuName = "Stat/EffectList", order = 3)]
    public class EffectValueListSO : ScriptableObject
    {
        //Ð§¹û
        public EffectValueSO[] effect;

        //ÃèÊö
        public string description;
    }
}
