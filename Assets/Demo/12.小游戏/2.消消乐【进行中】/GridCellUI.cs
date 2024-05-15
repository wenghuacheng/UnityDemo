using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.XXL
{
    /// <summary>
    /// 单元格UI
    /// </summary>
    public class GridCellUI : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer iconRenderer;

        /// <summary>
        /// 稳定后设置图标
        /// </summary>
        /// <param name="sprite"></param>
        public void SetIcon(Sprite sprite)
        {
            iconRenderer.sprite = sprite;
        }

    }
}