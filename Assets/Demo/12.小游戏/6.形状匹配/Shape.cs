using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.MatchShapes
{
    public class Shape : MonoBehaviour
    {
        [HideInInspector]
        public MatchShape CurrentShape { get; private set; }//��ǰ��״

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
            //���������ƶ�
            this.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}