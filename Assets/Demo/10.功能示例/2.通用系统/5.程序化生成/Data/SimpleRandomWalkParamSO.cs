using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.PCG
{
    /// <summary>
    /// ��������㷨����
    /// </summary>
    [CreateAssetMenu(menuName = "PCG/SimpleRandomWalkData", fileName = "SimpleRandomWalkParameter_")]
    public class SimpleRandomWalkParamSO : ScriptableObject
    {
        //�������������߳���
        public int iterationts = 10, walkLength = 10;
        public bool startRandomlyEachIteration = true;
    }
}
