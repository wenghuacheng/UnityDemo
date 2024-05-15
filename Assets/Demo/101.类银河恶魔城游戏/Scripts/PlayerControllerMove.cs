using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.MetroidvaniaGame
{
    public class PlayerControllerMove : MonoBehaviour
    {
        private Rigidbody2D body;
        private PlayerInputManager_VP pim;

        //�������
        private float xInput;
        private float yInput;
        private float jInput;//��Ծ
        private bool jInputPressing;//��Ծ����ס
        private float sInput;//��̼�
        private bool sInputPressing;//��̼���ס

        //��ɫ����
        private int facingDirection = 1; //1:���� -1������

        //�ٶȲ���
        private float moveSpeed = 3f;
        private float jumpHeight = 200f;
        private int airJump = 3;//������������

        //��Ծ״̬
        private bool isJumping;//��Ծ��
        private bool isJumpFall;//������
        private int jumpChance;//��Ծ����
        private bool jumpLong;//����
        private float JumpLongCoolDown;//������ȴ

        //��ɫ��ײ״̬
        private bool onTop;
        private bool onGround;
        private bool onOneWay;

        //��ɫ�������������
        Vector2 newVelocity = Vector2.zero;
        Vector2 newForce;

        //��������
        GroundSensor groundCheck;

        private void Awake()
        {
            //��ȡ��������
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
        /// ��鰴������
        /// </summary>
        private void CheckInput()
        {
            //PS:����������¼�ͬʱ��סʱ��yΪ0
            if (pim.GetPlayerInput(0) < 0 && pim.GetPlayerInput(1) > 0) yInput = 0;
            else yInput = pim.GetPlayerInput(0) < 0 ? pim.GetPlayerInput(0) : pim.GetPlayerInput(1);

            if (pim.GetPlayerInput(2) < 0 && pim.GetPlayerInput(3) > 0) xInput = 0;
            else xInput = pim.GetPlayerInput(2) < 0 ? pim.GetPlayerInput(2) : pim.GetPlayerInput(3);

            jInput = pim.GetPlayerInput(10);
            sInput = pim.GetPlayerInput(11);
        }

        /// <summary>
        /// ��ɫ�ƶ�
        /// </summary>
        private void PlayerMove()
        {
            //��ɫ������ж�
            MoveFunctionDetermine();
            //��ɫ��Ծ���ж�
            JumpFunctionDetermine();
        }

        /// <summary>
        /// �����ж��繥���ƶ�����¥��
        /// </summary>
        private void MoveFunctionDetermine()
        {
            //��ǰֻ�е������ƶ�����������������
            Move();
        }

        /// <summary>
        /// ��ɫˮƽ�ƶ��ٶȼ���
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
        /// ʵ���ƶ���ɫ
        /// </summary>
        private void ApplyMovement(Vector2 velocity)
        {
            float inputMoveX = velocity.x;
            float inputMoveY = velocity.y;

            newVelocity.Set(inputMoveX, onGround ? 0 : inputMoveY);

            body.velocity = newVelocity;
        }

        /// <summary>
        /// ��ɫ����
        /// </summary>
        private void Flip()
        {
            facingDirection *= -1;
            this.transform.localScale = new Vector3(facingDirection, this.transform.localScale.y, this.transform.localScale.z);
        }

        /// <summary>
        /// ��Ծ
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
        /// ������Ծ�����Ȳ���
        /// </summary>
        private void JumpReturnOnGround()
        {
            jumpChance = airJump;
        }

        /// <summary>
        /// ������
        /// </summary>
        private void CheckGround()
        {
            onGround = groundCheck.OnGround;
            onOneWay = groundCheck.OnOneWay;

            if (onGround) JumpReturnOnGround();
        }
    }
}