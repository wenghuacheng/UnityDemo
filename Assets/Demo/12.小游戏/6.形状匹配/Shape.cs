using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.MatchShapes
{
    public class Shape : MonoBehaviour
    {
        [HideInInspector]
        public MatchShape CurrentShape { get; private set; }//当前形状

        private SpriteRenderer _renderer;
        private float speed;

        public void Initialize(MatchShape shape, float speed)
        {
            _renderer = GetComponent<SpriteRenderer>();

            this.CurrentShape = shape;
            this.speed = speed;
            _renderer.sprite = shape.sprite;
        }

        void Update()
        {
            //从右向左移动
            this.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}