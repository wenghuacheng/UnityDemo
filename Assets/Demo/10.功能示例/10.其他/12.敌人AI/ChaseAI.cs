using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

namespace Demo.Misc
{
    public class ChaseAI : MonoBehaviour
    {
        [SerializeField] private float Raduis = 15;
        [SerializeField] private LayerMask playerMask;

        private Collider2D target = null;
        private bool isRayTarget = false;

        private Stack<Vector3> positions = new Stack<Vector3>();
        private Vector2 lastTargetPosition = Vector2.zero;
        private Vector3 returnTarget = Vector2.zero;
        private State state = State.Idle;

        private enum State
        {
            Idle, Chase, Return
        }

        private void Update()
        {
            DetectRaduis();
            DectectRay();
            Handle();

        }

        #region 检测
        /// <summary>
        /// 监测敌人
        /// </summary>
        private void DetectRaduis()
        {
            var collidor = Physics2D.OverlapCircle(this.transform.position, Raduis, playerMask);
            if (collidor == null)
                return;

            target = collidor;
        }

        /// <summary>
        /// 射线检测
        /// </summary>
        /// <returns></returns>
        private void DectectRay()
        {
            if (target == null) return;

            var direction = (target.transform.position - this.transform.position).normalized;
            var rayHitTarget = Physics2D.Raycast(this.transform.position, direction, Raduis);
            isRayTarget = rayHitTarget.collider == target;
        }

        #endregion

        #region 
        private void Handle()
        {
            //Debug.Log(isRayTarget);
            if (target != null && isRayTarget)
            {
                //移动
                state = State.Chase;
                this.transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, Time.deltaTime);
                positions.Push(this.transform.position);
                lastTargetPosition = target.transform.position;
            }
            else if (target != null && !isRayTarget && state == State.Chase)
            {
                //尝试走到最后一次发现目标的位置
                this.transform.position = Vector2.MoveTowards(this.transform.position, lastTargetPosition, Time.deltaTime);
                positions.Push(this.transform.position);

                if (Vector2.Distance(lastTargetPosition, this.transform.position) < 0.1f)
                {
                    //到达位置但是还是没发现目标则返回
                    target = null;
                    returnTarget = positions.Pop();
                }
            }
            else if (target == null && positions.Count > 0)
            {
                state = State.Return;
                //原路返回
                if (Vector2.Distance(returnTarget, this.transform.position) < 0.1f)
                {
                    returnTarget = positions.Pop();
                }
                this.transform.position = Vector2.MoveTowards(this.transform.position, returnTarget, Time.deltaTime);
            }
        }
        #endregion


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position, Raduis);

            if (target != null && isRayTarget)
            {
                Gizmos.color = Color.blue;
                var direction = (target.transform.position - this.transform.position).normalized;
                Gizmos.DrawLine(this.transform.position, target.transform.position);
            }
        }

    }
}