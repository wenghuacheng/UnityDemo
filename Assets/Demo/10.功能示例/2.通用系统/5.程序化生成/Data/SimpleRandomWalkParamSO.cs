using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.PCG
{
    /// <summary>
    /// 随机游走算法参数
    /// </summary>
    [CreateAssetMenu(menuName = "PCG/SimpleRandomWalkData", fileName = "SimpleRandomWalkParameter_")]
    public class SimpleRandomWalkParamSO : ScriptableObject
    {
        //迭代次数，游走长度
        public int iterationts = 10, walkLength = 10;
        public bool startRandomlyEachIteration = true;
    }
}
