using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Maps
{
    //[CreateAssetMenu(fileName = "RoomDoorway_", menuName = "���³�/��������")]
    [Serializable]
    public class RoomDoorway //: ScriptableObject
    {
        //����������ĵ�����
        public Vector2Int entrancePosition;

        //���½Ǳ߽�����
        public Vector2Int lowerBound;

        //���ϽǱ߽�����
        public Vector2Int upperBound;

        //�Ŷ��ķ���
        public DoorDirection direction;

        //�Ƿ��Ѿ�����
        public bool isConnected;
    }
}