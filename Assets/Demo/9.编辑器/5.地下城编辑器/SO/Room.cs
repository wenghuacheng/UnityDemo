using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Maps
{
    /// <summary>
    /// �������췿��
    /// </summary>
    public class Room //: MonoBehaviour
    {
        //�ڵ�ID
        public string id;

        //���ڵ�ID
        public string parentNodeId;

        //����Ԥ����
        public GameObject roomPrefab;

        //���½Ǳ߽�����
        public Vector2Int lowerBound;

        //���ϽǱ߽�����
        public Vector2Int upperBound;

        //ģ������±߽�
        public Vector2Int templateLowerBounds;
        public Vector2Int templateUpperBounds;

        //�Ŷ��б�
        public List<RoomDoorway> doorList = new List<RoomDoorway>();
    }
}