using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.MetroidvaniaGame
{
    public class PlayerControllerMove : MonoBehaviour
    {
        private Rigidbody2D body;
        private PlayerInputManager_VP pim;

        //玩家输入
        private float xInput;
        private float yInput;
        private float jInput;//跳跃
        private bool jInputPressing;//跳跃键按住
        private float sInput;//冲刺键
        private bool sInputPressing;//冲刺键按住

        //角色朝向
        private int facingDirection = 1; //1:正面 -1：反面

        //速度参数
        private float moveSpeed = 3f;
        private float jumpHeight = 200f;
        private int airJump = 3;//空中连跳次数

        //跳跃状态
        private bool isJumping;//跳跃中
        private bool isJumpFall;//掉落中
        private int jumpChance;//跳跃次数
        private bool jumpLong;//长跳
        private float JumpLongCoolDown;//长跳冷却

        //角色碰撞状态
        private bool onTop;
        private bool onGround;
        private bool onOneWay;

        //角色作用力，方向等
        Vector2 newVelocity = Vector2.zero;
        Vector2 newForce;

        //地面检测器
        GroundSensor groundCheck;

        private void Awake()
        {
            //获取地面检测器
            groundCheck = this.transform.GetChild(this.transform.childCount - 1).GetComponent<GroundSensor>();
            body = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            pim = PlayerInputManager_VP.pim;
        }

        private void Update()
        {
            CheckInput();
            CheckGround();
        }

        private void FixedUpdate()
        {
            PlayerMove();
            ApplyMovement(newVelocity);
        }

        /// <summary>
        /// 检查按键输入
        /// </summary>
        private void CheckInput()
        {
            //PS:这里代表上下键同时按住时，y为0
            if (pim.GetPlayerInput(0) < 0 && pim.GetPlayerInput(1) > 0) yInput = 0;
            else yInput = pim.GetPlayerInput(0) < 0 ? pim.GetPlayerInput(0) : pim.GetPlayerInput(1);

            if (pim.GetPlayerInput(2) < 0 && pim.GetPlayerInput(3) > 0) xInput = 0;
            else xInput = pim.GetPlayerInput(2) < 0 ? pim.GetPlayerInput(2) : pim.GetPlayerInput(3);

            jInput = pim.GetPlayerInput(10);
            sInput = pim.GetPlayerInput(11);
        }

        /// <summary>
        /// 角色移动
        /// </summary>
        private void PlayerMove()
        {
            //角色方向键判断
            MoveFunctionDetermine();
            //角色跳跃键判断
            JumpFunctionDetermine();
        }

        /// <summary>
        /// 功能判定如攻击移动，盘楼梯
        /// </summary>
        private void MoveFunctionDetermine()
        {
            //当前只有单纯的移动，后面可以添加条件
            Move();
        }

        /// <summary>
        /// 角色水平移动速度计算
        /// </summary>
        private void Move()
        {
            float move = onGround && yInput > 0.1f ? 0 : moveSpeed * xInput;

            if (xInput == 1 && facingDirection == -1 ||
                xInput == -1 && facingDirection == 1)
            {              
                Flip();
            }

            newVelocity.Set(move, body.velocity.y);
        }

        /// <summary>
        /// 实际移动角色
        /// </summary>
        private void ApplyMovement(Vector2 velocity)
        {
            float inputMoveX = velocity.x;
            float inputMoveY = velocity.y;

            newVelocity.Set(inputMoveX, onGround ? 0 : inputMoveY);

            body.velocity = newVelocity;
        }

        /// <summary>
        /// 角色朝向
        /// </summary>
        private void Flip()
        {
            facingDirection *= -1;
            this.transform.localScale = new Vector3(facingDirection, this.transform.localScale.y, this.transform.localScale.z);
        }

        /// <summary>
        /// 跳跃
        /// </summary>
        private void JumpFunctionDetermine()
        {
            if (onGround && jInput > 0)
            {
                newForce.Set(0, jumpHeight);
                body.AddForce(newForce, ForceMode2D.Force);
            }
        }

        /// <summary>
        /// 重置跳跃次数等参数
        /// </summary>
        private void JumpReturnOnGround()
        {
            jumpChance = airJump;
        }

        /// <summary>
        /// 地面检测
        /// </summary>
        private void CheckGround()
        {
            onGround = groundCheck.OnGround;
            onOneWay = groundCheck.OnOneWay;

            if (onGround) JumpReturnOnGround();
        }
    }
}