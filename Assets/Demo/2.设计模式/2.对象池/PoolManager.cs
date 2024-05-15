using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Demo.Patterns
{
    public class PoolManager : MonoBehaviour
    {
        //用于生成的预制体
        /**
         特别注意这里需要使用gameobject类型，直接使用脚本类型容易在Instantiate时出现类型转换错误
         */
        [SerializeField] private GameObject prefab;

        private ObjectPool<TestPoolObject> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<TestPoolObject>(ObjectCreate, ObjectGet, ObjectRelease, ObjectDestroy
                , defaultCapacity: 2, maxSize: 10);
        }


        /// <summary>
        /// 对象池创建对象
        /// </summary>
        private TestPoolObject ObjectCreate()
        {
            var obj = Instantiate(prefab);
            var t = obj.GetComponent<TestPoolObject>();
            t.SetPool(_pool);
            return t;
        }

        /// <summary>
        /// 对象池获取对象
        /// </summary>
        /// <param name="obj"></param>
        private void ObjectGet(TestPoolObject obj)
        {
            //重置物体位置等初始信息
            obj.Reset();
            obj.gameObject.SetActive(true);
        }

        /// <summary>
        /// 对象返回对象池
        /// </summary>
        /// <param name="obj"></param>
        private void ObjectRelease(TestPoolObject obj)
        {
            obj.gameObject.SetActive(false);
        }

        /// <summary>
        /// 对象从对象池中销毁
        /// </summary>
        private void ObjectDestroy(TestPoolObject obj)
        {
            Destroy(obj.gameObject);
        }


        #region 生成对象测试代码

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                var obj = _pool.Get();
                obj.transform.position = new Vector3(0, 2, 0);
            }

        }
        #endregion

    }
}