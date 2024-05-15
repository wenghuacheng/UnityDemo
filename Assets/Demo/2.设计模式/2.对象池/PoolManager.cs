using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Demo.Patterns
{
    public class PoolManager : MonoBehaviour
    {
        //�������ɵ�Ԥ����
        /**
         �ر�ע��������Ҫʹ��gameobject���ͣ�ֱ��ʹ�ýű�����������Instantiateʱ��������ת������
         */
        [SerializeField] private GameObject prefab;

        private ObjectPool<TestPoolObject> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<TestPoolObject>(ObjectCreate, ObjectGet, ObjectRelease, ObjectDestroy
                , defaultCapacity: 2, maxSize: 10);
        }


        /// <summary>
        /// ����ش�������
        /// </summary>
        private TestPoolObject ObjectCreate()
        {
            var obj = Instantiate(prefab);
            var t = obj.GetComponent<TestPoolObject>();
            t.SetPool(_pool);
            return t;
        }

        /// <summary>
        /// ����ػ�ȡ����
        /// </summary>
        /// <param name="obj"></param>
        private void ObjectGet(TestPoolObject obj)
        {
            //��������λ�õȳ�ʼ��Ϣ
            obj.Reset();
            obj.gameObject.SetActive(true);
        }

        /// <summary>
        /// ���󷵻ض����
        /// </summary>
        /// <param name="obj"></param>
        private void ObjectRelease(TestPoolObject obj)
        {
            obj.gameObject.SetActive(false);
        }

        /// <summary>
        /// ����Ӷ����������
        /// </summary>
        private void ObjectDestroy(TestPoolObject obj)
        {
            Destroy(obj.gameObject);
        }


        #region ���ɶ�����Դ���

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