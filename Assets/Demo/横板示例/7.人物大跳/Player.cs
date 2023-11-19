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

        //最大跳跃持续时间
        private float maxJumpTime = 1f;
        private float jumpTime = 0f;

        private float jumpForce = 2f;
        //标志是否起跳【防止再空中再次点击跳跃】
        private bool isStartJump = false;

        private void Update()
        {
            //刷新地面状态
            CheckGround();
            //跳跃逻辑
            Jump();
        }

        /// <summary>
        /// 跳跃
        /// 这里只演示垂直起跳，不考虑水平起跳
        /// </summary>
        private void Jump()
        {
            var isJumpPressing = Input.GetKey(KeyCode.Space);
            var jumpKeyDown = Input.GetKeyDown(KeyCode.Space);

            if (jumpKeyDown && isOnGround)
            {
                //起跳
                Debug.Log("起跳");
                jumpTime = maxJumpTime;
                rb.velocity = this.transform.up * jumpForce;
                isStartJump = true;
            }
            //起跳后持续按住跳跃键并且没有松开跳跃键
            else if (!isOnGround && isJumpPressing && isStartJump)
            {
                //起跳后持续按住跳跃键
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

            //松开后只有着地后才能重新设置
            if (Input.GetKeyUp(KeyCode.Space) && !isOnGround)
            {
                isStartJump = false;
            }
        }


        #region 地面检测
        private bool CheckGround()
        {
            var hit2D = Physics2D.Raycast(checkGroundPosition.position, transform.up * -1, checkDistance, whatIsGround);
            isOnGround = hit2D.collider != null;
            //Debug.Log("着地状态:" + isOnGround);
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