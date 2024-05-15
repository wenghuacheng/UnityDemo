using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.DungeonEditor
{
    /// <summary>
    /// ����ڵ�����
    /// ���ڵ��ڱ༭����ѡ��
    /// </summary>
    [CreateAssetMenu(fileName = "RoomNodeType_", menuName = "��UI��ʾ/���³�/��������")]
    public class RoomNodeTypeSO : ScriptableObject
    {
        public string roomName;

        //�Ƿ�ʱͨ������
        public bool isCorridor;

        //�Ƿ����ϱ����������
        public bool isCorridorNS;

        //�Ƿ��Ƕ������������
        public bool isCorridorEW;

        //�Ƿ������
        public bool isEntrance;

        //�Ƿ��Ƿ�������
        public bool isRoom;

        //Ĭ��
        public bool isNone;

        //����ֻ����ʾ����
        ////�Ƿ���Boss����
        //public bool isBossRoom;

        ////�Ƿ���С����
        //public bool isSmallRoom;

        ////�Ƿ������ͷ���
        //public bool isMediumRoom;

        ////�Ƿ��Ǵ��ͷ���
        //public bool isBigRoom;
        
    }

}