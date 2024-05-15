using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Demo.Patterns
{
    public class TestPoolObject : MonoBehaviour
    {
        public ObjectPool<TestPoolObject> _pool;

        private float time = 0;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //3����ͷ�
            time += Time.deltaTime;
            if (time > 3)
                this._pool.Release(this);
        }

        /// <summary>
        /// ���øö������ڶ����
        /// </summary>
        /// <param name="_pool"></param>
        public void SetPool(ObjectPool<TestPoolObject> pool)
        {
            this._pool = pool;
        }

        /// <summary>
        /// ���ڸ��ú����ö���
        /// </summary>
        public void Reset()
        {
            this.time = 0;
            this.transform.position = Vector3.zero;
        }
    }
}