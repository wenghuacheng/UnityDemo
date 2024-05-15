using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.XXL
{
    /// <summary>
    /// ��Ԫ��UI
    /// </summary>
    public class GridCellUI : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer iconRenderer;

        /// <summary>
        /// �ȶ�������ͼ��
        /// </summary>
        /// <param name="sprite"></param>
        public void SetIcon(Sprite sprite)
        {
            iconRenderer.sprite = sprite;
        }

    }
}