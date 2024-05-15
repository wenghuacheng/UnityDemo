using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Games.Shoot2D
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsEnemy;

        private Camera _camera;
        private int score;//�÷�

        [SerializeField] private TextMeshProUGUI text;

        void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            Shoot();
        }

        /// <summary>
        /// �����Ļ�������
        /// </summary>
        private void Shoot()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            var hitInfo = Physics2D.Raycast(ray.origin, Vector2.zero, Mathf.Infinity);
            if (hitInfo.collider != null)
            {
                var enemy = hitInfo.collider.GetComponent<Enemy>();
                if (enemy == null) return;

                var health = enemy.TakeDamage();
                if (health <= 0)
                {
                    //�÷�
                    score += 1;
                    text.text = score.ToString();
                }
            }
        }
    }
}
