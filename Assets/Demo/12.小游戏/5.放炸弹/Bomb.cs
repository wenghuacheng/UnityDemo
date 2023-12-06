using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Demo.Games.PlaceBomb
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private LayerMask whatIsWall;
        [SerializeField] private float raduis = 2;

        private float time;

        void Start()
        {
            time = 3f;
        }

        void Update()
        {
            CountDown();
        }

        private void CountDown()
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                DestroyWall();
                Destroy(this.gameObject);
            }
            else
            {
                //刷新时间
                text.text = Mathf.RoundToInt(time).ToString();
            }
        }

        /// <summary>
        /// 判定可摧毁的墙体
        /// </summary>
        private void DestroyWall()
        {
            //判定圆形范围内的墙体
            var hit2DList = Physics2D.OverlapCircleAll(this.transform.position, raduis, whatIsWall);
            Debug.Log(hit2DList.Count());
            foreach (var hit2D in hit2DList)
            {
                if (hit2D != null)
                    Destroy(hit2D.gameObject,0.1f);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(this.transform.position, raduis);
        }
    }
}