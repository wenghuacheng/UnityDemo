using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Demo.HB.Player.LargeJump
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Transform checkGroundPosition;
        [SerializeField] private Rigidbody2D rb;

        private bool isOnGround;
        private float checkDistance = 0.01f;

        //�����Ծ����ʱ��
        private float maxJumpTime = 1f;
        private float jumpTime = 0f;

        private float jumpForce = 2f;
        //��־�Ƿ���������ֹ�ٿ����ٴε����Ծ��
        private bool isStartJump = false;

        private void Update()
        {
            //ˢ�µ���״̬
            CheckGround();
            //��Ծ�߼�
            Jump();
        }

        /// <summary>
        /// ��Ծ
        /// ����ֻ��ʾ��ֱ������������ˮƽ����
        /// </summary>
        private void Jump()
        {
            var isJumpPressing = Input.GetKey(KeyCode.Space);
            var jumpKeyDown = Input.GetKeyDown(KeyCode.Space);

            if (jumpKeyDown && isOnGround)
            {
                //����
                Debug.Log("����");
                jumpTime = maxJumpTime;
                rb.velocity = this.transform.up * jumpForce;
                isStartJump = true;
            }
            //�����������ס��Ծ������û���ɿ���Ծ��
            else if (!isOnGround && isJumpPressing && isStartJump)
            {
                //�����������ס��Ծ��
                jumpTime -= Time.deltaTime;
                if (jumpTime > 0)
                {
                    rb.velocity = this.transform.up * jumpForce;
                }
                else
                {
                    isStartJump = false;
                }
            }

            //�ɿ���ֻ���ŵغ������������
            if (Input.GetKeyUp(KeyCode.Space) && !isOnGround)
            {
                isStartJump = false;
            }
        }


        #region ������
        private bool CheckGround()
        {
            var hit2D = Physics2D.Raycast(checkGroundPosition.position, transform.up * -1, checkDistance, whatIsGround);
            isOnGround = hit2D.collider != null;
            //Debug.Log("�ŵ�״̬:" + isOnGround);
            return isOnGround;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var to = checkGroundPosition.position + transform.up * -1 * checkDistance;
            Gizmos.DrawLine(checkGroundPosition.position, to);
        }
        #endregion


    }
}