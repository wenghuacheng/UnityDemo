using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.ResourcesLoadDemo
{
    /// <summary>
    /// �����Resource�ļ����µ���Դ
    /// ���⣺���ֻ�ܼ���2G����Դ���ݣ��Ὣ������Դ�������Ϸ�������Ƿ�ʹ�ã�ֻ�ܼ���Resources�ļ����е���Դ���ܼ����ⲿ�ļ��е���Դ
    /// ���Բ�����
    /// </summary>
    public class ResourcesLoad : MonoBehaviour
    {
        private GameObject prefab;

        void Update()
        {
            //�������ɶ���
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(prefab);
            }
        }

        private void OnEnable()
        {
            //��Resource�ļ����¼���
            prefab = Resources.Load("ResourcesLoadDemo/TestObj") as GameObject;
        }

        private void OnDisable()
        {
            //ж�ز��õ���Դ
            Resources.UnloadUnusedAssets();
        }
    }
}