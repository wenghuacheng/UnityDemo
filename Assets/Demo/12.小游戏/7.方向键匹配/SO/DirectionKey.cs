using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.MatchDirection
{
    [CreateAssetMenu(fileName = "Key_", menuName = "С��Ϸ/ƥ����Ϸ/�����")]
    public class DirectionKey : ScriptableObject
    {
        public KeyCode[] sequence;

        public GameObject prefab;

        /// <summary>
        /// ����ƥ��
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool MatchKeys(List<KeyCode> input)
        {
            if (sequence.Length != input.Count) return false;

            var orderSequence = sequence.OrderBy(p => p).ToArray();
            var orderInput = input.OrderBy(p => p).ToArray();

            for (int i = 0; i < sequence.Length; i++)
            {
                if (orderSequence[i] != orderInput[i])
                    return false;
            }
            return true;
        }
    }
}