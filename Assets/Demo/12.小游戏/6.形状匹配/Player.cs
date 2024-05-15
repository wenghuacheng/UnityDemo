using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.Games.MatchShapes
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private MatchShapeList shapeList;
        [SerializeField] private TextMeshProUGUI text;

        private int index = 0;//��ǰ��״����
        private MatchShape currentShape;//��ǰ��״
        private SpriteRenderer _renderer;
        private int score = 0;//����

        void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            RefreshSprite();
        }

        void Update()
        {
            ChangeShape();
        }

        #region �����״���
        /// <summary>
        /// �ı���״
        /// </summary>
        private void ChangeShape()
        {
            //���Ҽ������л�
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                index--;
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                index++;
            else
                return;

            if (index < 0)
                index = shapeList.list.Length - 1;
            index = index % shapeList.list.Length;

            RefreshSprite();
        }

        /// <summary>
        /// ˢ�¾���
        /// </summary>
        private void RefreshSprite()
        {
            currentShape = shapeList.list[index];
            _renderer.sprite = currentShape.sprite;
        }
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var shape = collision.gameObject.GetComponent<Shape>();
            if (shape != null)
            {
                var goal = shape.CurrentShape.id == currentShape.id ? 1 : 0;
                Debug.Log($"{shape.CurrentShape.id }::{currentShape.id}::{ shape.CurrentShape.id == currentShape.id}");
                score += goal;
                RefreshScore();
            }
            Destroy(collision.gameObject);
        }

        /// <summary>
        /// ˢ�µ÷�
        /// </summary>
        private void RefreshScore()
        {
            text.text = score.ToString();
        }
    }
}