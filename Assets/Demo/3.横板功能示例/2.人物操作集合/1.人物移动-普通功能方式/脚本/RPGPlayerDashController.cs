using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.Ability
{
    /// <summary>
    /// ��̼���
    /// </summary>
    public class RPGPlayerDashController : MonoBehaviour
    {
        [SerializeField] private float dashSpeed = 50f;
        [SerializeField] private float dashDurationTime = 1f;
        [SerializeField] private Rigidbody2D rb;

        private float dashTime;

        public bool IsDashing { get { return dashTime > 0; } }

        void Update()
        {
            dashTime -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //����ʱ���ó��ʱ��
                dashTime = dashDurationTime;
                Debug.Log("���");
            }

            if (IsDashing)
            {
                float x = Input.GetAxisRaw("Horizontal");
                rb.velocity = new Vector2(x * dashSpeed, rb.velocity.y);
            }
        }
    }
}