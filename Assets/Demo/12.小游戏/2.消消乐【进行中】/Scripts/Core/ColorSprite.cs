using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.Pieces
{
    /// <summary>
    /// 颜色对应的精灵
    /// </summary>
    [Serializable]
    public class ColorSprite
    {
        public ColorType colorType;
        public SpriteRenderer sprite;
    }
}