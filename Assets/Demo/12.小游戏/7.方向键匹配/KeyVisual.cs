using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.MatchDirection
{
    /// <summary>
    /// 按键显示控制
    /// </summary>
    public class KeyVisual : MonoBehaviour
    {
        private List<SpriteRenderer> sprites = new List<SpriteRenderer>();

        void Start()
        {
            sprites.AddRange(this.GetComponentsInChildren<SpriteRenderer>());
        }


        /// <summary>
        /// 成功匹配，变色
        /// </summary>
        public void MatchKey()
        {
            foreach (var sprite in sprites)
            {
                sprite.color = Color.red;
            }
        }
    }
}