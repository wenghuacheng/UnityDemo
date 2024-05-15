using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.MatchDirection
{
    /// <summary>
    /// ������ʾ����
    /// </summary>
    public class KeyVisual : MonoBehaviour
    {
        private List<SpriteRenderer> sprites = new List<SpriteRenderer>();

        void Start()
        {
            sprites.AddRange(this.GetComponentsInChildren<SpriteRenderer>());
        }


        /// <summary>
        /// �ɹ�ƥ�䣬��ɫ
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