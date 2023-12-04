using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.BrickBreaker
{
    /// <summary>
    /// 玩家平台控制
    /// </summary>
    public class Platform : MonoBehaviour
    {
        [SerializeField] private float speed = 25;//平台移动速度

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
        /// 左右移动
        /// </summary>
        private void InputController()
        {
            var x = Input.GetAxis("Horizontal");
            _rb.velocity = new Vector2(x * speed, 0);
        }

    }
}