using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.MatchDirection
{
    [CreateAssetMenu(fileName = "Key_", menuName = "小游戏/匹配游戏/方向键")]
    public class DirectionKey : ScriptableObject
    {
        public KeyCode[] sequence;

        public GameObject prefab;

        /// <summary>
        /// 按键匹配
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