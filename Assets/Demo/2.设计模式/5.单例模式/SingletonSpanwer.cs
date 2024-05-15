using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Patterns
{
    /// <summary>
    /// ����ģʽ������
    /// </summary>
    public class SingletonSpanwer : MonoBehaviour
    {
        //�Ƿ��Ѿ���ʼ������
        private static bool isSpanwed = false;

        //�����˵��������Ԥ����
        [SerializeField] GameObject persistentObjectPerfab;

        private void Awake()
        {
            //�Ѿ���ʼ������
            if (isSpanwed) return;

            //��ʼ��Ԥ���壬Ԥ�����ϵĽű����ǵ���
            GameObject gameObject = Instantiate(persistentObjectPerfab);
            DontDestroyOnLoad(gameObject);

            /**
             ����ͨ���¼��������������Ϣ
             */

            isSpanwed = true;
        }

    }

}