using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.BrickBreaker
{
    /// <summary>
    /// ���ƽ̨����
    /// </summary>
    public class Platform : MonoBehaviour
    {
        [SerializeField] private float speed = 25;//ƽ̨�ƶ��ٶ�

        private Rigidbody2D _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            InputController();
        }

        /// <summary>
        /// �����ƶ�
        /// </summary>
        private void InputController()
        {
            var x = Input.GetAxis("Horizontal");
            _rb.velocity = new Vector2(x * speed, 0);
        }

    }
}