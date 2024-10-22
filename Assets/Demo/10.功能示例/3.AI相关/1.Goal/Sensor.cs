using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.AI.Goal
{
    /// <summary>
    /// 传感器（检测是否满足条件）
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public class Sensor : MonoBehaviour
    {
        public const string PlayerTag = "Player";

        [SerializeField] float dectectRadius = 5f;//检测范围
        [SerializeField] float timerInterval = 1f;//检测间隔时间

        private CircleCollider2D detectCollider;
        private GameObject target;
        private Vector3 lastKnownPosition;//最后一个目标位置


        //目标位置发生移动时触发
        public event Action OnTargetChanged = delegate { };

        //目标位置
        public Vector3 TargetPosition => target ? target.transform.position : Vector3.zero;

        public bool IsTargetInRange => TargetPosition != Vector3.zero;

        private void Awake()
        {
            detectCollider = GetComponent<CircleCollider2D>();
            detectCollider.isTrigger = true;
            detectCollider.radius = dectectRadius;
        }

        private void Start()
        {
            StartCoroutine("Detect");
        }


        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private IEnumerator Detect()
        {
            while (true)
            {
                UpdateTargetPosition(target);
                yield return new WaitForSeconds(timerInterval);
            }
        }

        /// <summary>
        /// 更新目标
        /// </summary>
        /// <param name="target"></param>
        private void UpdateTargetPosition(GameObject target = null)
        {
            this.target = target;
            if (IsTargetInRange && (lastKnownPosition != TargetPosition || lastKnownPosition != Vector3.zero))//这个判断条件有点疑惑
            {
                //只有目标是新的或目标移动了才触发事件
                lastKnownPosition = TargetPosition;
                OnTargetChanged?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(PlayerTag)) return;
            UpdateTargetPosition(collision.gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag(PlayerTag)) return;
            UpdateTargetPosition();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = IsTargetInRange ? Color.red : Color.green;
            Gizmos.DrawWireSphere(transform.position, dectectRadius);
        }
#endif
    }
}
