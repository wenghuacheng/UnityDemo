using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ���˵������
    /// ��Ŀǰ�������ʾ������ͨ��������GameManager�ķ�ʽ�ڻ�ɱ��������Ӿ���/��Ʒ��
    /// </summary>
    public class EnemyLoot : MonoBehaviour
    {
        [Header("Config")]
        private float expDrop;

        //��ɱ�����þ���
        public float ExpDrop => expDrop;
    }
}