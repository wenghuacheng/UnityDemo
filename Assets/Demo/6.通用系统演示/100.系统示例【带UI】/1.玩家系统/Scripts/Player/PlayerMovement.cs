using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// �ƶ����ƽű�
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float speed;

        //���ɵ�������ϵͳ�ű�
        private PlayerActions actions;
        private Rigidbody2D rb;
        private PlayerAnimations playerAnimations;
        private Player player;

        private Vector2 moveDirection;
        public Vector2 MoveDirection => moveDirection;

        private void Awake()
        {
            actions = new PlayerActions();
            rb = GetComponent<Rigidbody2D>();
            playerAnimations = GetComponent<PlayerAnimations>();
            player = GetComponent<Player>();
        }

        private void Update()
        {
            ReadMovement();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnEnable()
        {
            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Disable();
        }

        /// <summary>
        /// �ƶ�
        /// </summary>
        private void Move()
        {
            if (player.Stats.Health <= 0) return;
            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// ��ȡ����ϵͳ�еİ���ֵ
        /// </summary>
        private void ReadMovement()
        {
            moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;
            if (moveDirection == Vector2.zero)
            {
                //������ʱ����Ч������������
                playerAnimations.SetMoveBoolTransition(false);
                return;
            }

            playerAnimations.SetMoveBoolTransition(true);
            playerAnimations.SetMoveAnimation(moveDirection);
        }
    }
}