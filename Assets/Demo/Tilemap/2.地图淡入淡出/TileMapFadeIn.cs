using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Demo.Maps
{
    /// <summary>
    /// µÿÕºµ≠»Î
    /// </summary>
    public class TileMapFadeIn : MonoBehaviour
    {
        private TilemapRenderer tilemapRenderer;

        void Start()
        {
            tilemapRenderer = GetComponent<TilemapRenderer>();
            tilemapRenderer.material.SetFloat("Alpha_Slider", 0.5f);
        }

        private IEnumerator StartMapFadeIn()
        {
            return null;
        }
    }
}