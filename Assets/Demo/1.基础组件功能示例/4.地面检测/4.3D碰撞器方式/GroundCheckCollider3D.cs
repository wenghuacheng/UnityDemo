using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroundDetection
{
    public class GroundCheckCollider3D : MonoBehaviour
    {
        private bool isGround = false;
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Jump();
            Debug.Log(isGround);
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var jumpHeight = 3;
                var y = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);//�˷���������Ծ��ʽ
                rb.velocity = new Vector3(0, y, 0);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            EvaluateCollision(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            EvaluateCollision(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            isGround = false;
        }

        private void EvaluateCollision(Collision collision)
        {
            isGround = false;
            for (int i = 0; i < collision.contactCount; i++)
            {
                var contact = collision.GetContact(i);
                //����ŵط���Ӧ���ǣ�0��1��0�����ж�ʱ������ô�ϸ�
                isGround |= contact.normal.y >= 0.9f;
            }
        }
    }
}