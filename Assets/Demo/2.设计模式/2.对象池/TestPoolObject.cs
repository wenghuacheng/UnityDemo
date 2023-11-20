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
            //3秒后释放
            time += Time.deltaTime;
            if (time > 3)
                this._pool.Release(this);
        }

        /// <summary>
        /// 设置该对象所在对象池
        /// </summary>
        /// <param name="_pool"></param>
        public void SetPool(ObjectPool<TestPoolObject> pool)
        {
            this._pool = pool;
        }

        /// <summary>
        /// 用于复用后重置对象
        /// </summary>
        public void Reset()
        {
            this.time = 0;
            this.transform.position = Vector3.zero;
        }
    }
}