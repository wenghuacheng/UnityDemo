using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.Pieces
{
    /// <summary>
    /// 地块颜色与预制体管理（可以考虑变成单例，现在是挂载地块预制体上）
    /// </summary>
    public class ColorPiece : MonoBehaviour
    {
        public ColorSprite[] list;
        private Dictionary<ColorType, SpriteRenderer> colorSpriteDict = new Dictionary<ColorType, SpriteRenderer>();

        private SpriteRenderer sprite;

        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();

            for (int i = 0; i < list.Length; i++)
            {
                var item = list[i];
                if (!colorSpriteDict.ContainsKey(item.colorType))
                {
                    colorSpriteDict.Add(item.colorType, item.sprite);
                }
            }
        }


        private ColorType color;

        public ColorType Color
        {
            get { return color; }
            set { SetColor(value); }
        }

        public int NumColors
        {
            get { return list.Length; }
        }


        public void SetColor(ColorType newColor)
        {
            color = newColor;
            if (colorSpriteDict.ContainsKey(color))
            {
                //这里没有素材，只能使用颜色替换
                sprite.color = colorSpriteDict[color].color;
            }
        }

    }
}