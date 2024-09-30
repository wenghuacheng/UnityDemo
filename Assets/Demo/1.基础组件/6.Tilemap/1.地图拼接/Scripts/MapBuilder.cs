using Demo.DungeonEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Maps
{
#if UNITY_EDITOR
    public class MapBuilder : MonoBehaviour
    {
        //���Ƶĵ�ͼ����
        //������Ϊ��һ���ؿ��ĵ�ͼ������ֻ��ʾһ���ؿ��ж�������ƴ��
        [SerializeField] private RoomGraphCanvas canvas;

        //�����ڵ�ͼ���Ƶ�ģ��
        [SerializeField] private RoomTemplateSO[] roomTemplateList;

        //�����б�
        private List<Room> roomList = new List<Room>();


        private void Awake()
        {
            BuildMap();
        }

        #region ��ͼ����
        /// <summary>
        /// ���Ƶ�ͼ
        /// </summary>
        public void BuildMap()
        {
            var entranceNode = FindEntrance(canvas);
            if (entranceNode == null) return;

            Queue<RoomNodeSO> roomQueue = new Queue<RoomNodeSO>();
            roomQueue.Enqueue(entranceNode);
            ProcessRoomLevel(roomQueue);
        }

        /// <summary>
        /// һ���Ĵ�����
        /// </summary>
        /// <param name="roomQueue"></param>
        private void ProcessRoomLevel(Queue<RoomNodeSO> roomQueue)
        {
            while (roomQueue.Count > 0)
            {
                var roomNode = roomQueue.Dequeue();

                //����ǰ������ӷ�����뵽�б��У��ɺ������д���
                foreach (var childId in roomNode.childNodeIdList)
                {
                    var childNode = canvas.GetNode(childId);
                    roomQueue.Enqueue(childNode);
                }

                var roomTemplate = FindRoomTemplate(roomNode);
                if (roomTemplate == null) continue;

                //�������ڻ��Ƶķ������
                var room = CreateRoom(roomNode, roomTemplate);
                HandleRoom(room);
                roomList.Add(room);
            }

            //���ɷ���
            InitializeRoom();
        }

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="room"></param>
        private void HandleRoom(Room room)
        {
            if (!string.IsNullOrWhiteSpace(room.parentNodeId))
            {
                //��������
                var parentRoom = roomList.FirstOrDefault(p => p.id == room.parentNodeId);
                if (parentRoom == null)
                {
                    Debug.Log($"{room.id}δ��ȡ���������");
                    return;
                }

                var parentDoorway = MatchParentDoorWay(parentRoom, room);
                if (parentDoorway == null)
                {
                    Debug.Log($"{room.id}û��ƥ����Ŷ�");
                    return;
                }

                PlaceRoom(parentRoom, room, parentDoorway);
            }
            else
            {
                //���
                PlaceRoom(room);
            }
        }

        /// <summary>
        /// ���÷���λ��
        /// </summary>
        private void PlaceRoom(Room parentRoom, Room room, RoomDoorway parentDoorway)
        {
            //�ҵ��븸�����෴������Ŷ���ƴ��
            var oppsiteDoor = GetOppositeDirection(parentDoorway.direction);
            var doorway = room.doorList.FirstOrDefault(p => !p.isConnected && p.direction == oppsiteDoor);
            if (doorway == null) return;

            //�Ŷ�����������λ��
            Vector2Int parentDoorwayPosition = parentRoom.lowerBound + parentDoorway.entrancePosition - parentRoom.templateLowerBounds;

            Vector2Int adjustment = Vector2Int.zero;

            //����Ŷ��򶫣����Ŷ���������ǰ�Ŷ��ڸ��Ŷ�����࣬��Ҫ���ұߵ���һ����λ[��X-1]
            switch (doorway.direction)
            {
                case DoorDirection.North:
                    adjustment = new Vector2Int(0, -1);
                    break;
                case DoorDirection.East:
                    adjustment = new Vector2Int(-1, 0);
                    break;
                case DoorDirection.South:
                    adjustment = new Vector2Int(0, 1);
                    break;
                case DoorDirection.West:
                    adjustment = new Vector2Int(1, 0);
                    break;
            }

            //�����Ŷ�λ�ü��㷿�����С�����λ��
            room.lowerBound = parentDoorwayPosition + adjustment + room.templateLowerBounds - doorway.entrancePosition;
            room.upperBound = room.lowerBound + room.templateUpperBounds - room.templateLowerBounds;

            //doorway.isConnected = true;
            //parentDoorway.isConnected = true;
        }

        /// <summary>
        /// ���÷��䡾��ڡ�
        /// </summary>
        /// <param name="room"></param>
        private void PlaceRoom(Room room)
        {
            room.lowerBound = room.templateLowerBounds;
            room.upperBound = room.lowerBound + room.templateUpperBounds - room.templateLowerBounds;
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitializeRoom()
        {
            //for (int i = 0; i < roomList.Count; i++)
            for (int i = 0; i < 2; i++)
            {
                var room = roomList[i];
                Vector3 roomPosition = new Vector3(room.lowerBound.x - room.templateLowerBounds.x, room.lowerBound.y - room.templateLowerBounds.y);
                Instantiate(room.roomPrefab, roomPosition, Quaternion.identity, this.transform);

                Debug.Log($"lowerBound:{room.lowerBound},upperBound:{room.upperBound}");
            }
        }

        #endregion

        #region ���߷���
        /// <summary>
        /// �ҵ���ͼ���
        /// </summary>
        /// <param name="canvas"></param>
        private RoomNodeSO FindEntrance(RoomGraphCanvas canvas)
        {
            var entrance = canvas.nodeList.FirstOrDefault(p => string.IsNullOrWhiteSpace(p.parentNodeId));
            return entrance;
        }

        /// <summary>
        /// ��ѯ����ģ��
        /// </summary>
        /// <param name="roomNode"></param>
        private RoomTemplateSO FindRoomTemplate(RoomNodeSO roomNode)
        {
            var roomTemplate = roomTemplateList.FirstOrDefault(p => p.roomNodeType == roomNode.selectedRoomNodeType);
            return roomTemplate;
        }

        /// <summary>
        /// ƥ��
        /// </summary>
        /// <param name="parentRoom"></param>
        private RoomDoorway MatchParentDoorWay(Room parentRoom, Room room)
        {
            var unconnectParentDoorway = parentRoom.doorList.Where(p => !p.isConnected);
            var unconnectDoorway = room.doorList.Where(p => !p.isConnected);

            foreach (var door in unconnectDoorway)
            {
                var parentDoorway = unconnectParentDoorway.FirstOrDefault(p => GetOppositeDirection(p.direction) == door.direction);
                if (parentDoorway != null)
                    return parentDoorway;
            }
            return null;
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="direction"></param>
        private DoorDirection GetOppositeDirection(DoorDirection direction)
        {
            switch (direction)
            {
                case DoorDirection.North:
                    return DoorDirection.South;
                case DoorDirection.South:
                    return DoorDirection.North;
                case DoorDirection.East:
                    return DoorDirection.West;
                case DoorDirection.West:
                    return DoorDirection.East;
                default:
                    return DoorDirection.West;
            }
        }

        /// <summary>
        /// �����������
        /// </summary>
        private Room CreateRoom(RoomNodeSO roomNode, RoomTemplateSO roomTemplate)
        {
            Room room = new Room();
            room.roomPrefab = roomTemplate.roomPrefab;
            room.templateLowerBounds = roomTemplate.lowerBound;
            room.templateUpperBounds = roomTemplate.upperBound;
            room.lowerBound = roomTemplate.lowerBound;
            room.upperBound = roomTemplate.upperBound;
            room.id = roomNode.id;
            room.parentNodeId = roomNode.parentNodeId;
            room.doorList = roomTemplate.doorList;

            return room;
        }
        #endregion
    }
#endif
}