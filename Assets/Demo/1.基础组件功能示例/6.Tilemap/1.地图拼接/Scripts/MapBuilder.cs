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
        //绘制的地图画布
        //可以认为是一个关卡的地图，这里只演示一个关卡中多个区域的拼接
        [SerializeField] private RoomGraphCanvas canvas;

        //可用于地图绘制的模板
        [SerializeField] private RoomTemplateSO[] roomTemplateList;

        //房间列表
        private List<Room> roomList = new List<Room>();


        private void Awake()
        {
            BuildMap();
        }

        #region 地图构建
        /// <summary>
        /// 绘制地图
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
        /// 一层层的处理房间
        /// </summary>
        /// <param name="roomQueue"></param>
        private void ProcessRoomLevel(Queue<RoomNodeSO> roomQueue)
        {
            while (roomQueue.Count > 0)
            {
                var roomNode = roomQueue.Dequeue();

                //将当前房间的子房间放入到列表中，由后续进行处理
                foreach (var childId in roomNode.childNodeIdList)
                {
                    var childNode = canvas.GetNode(childId);
                    roomQueue.Enqueue(childNode);
                }

                var roomTemplate = FindRoomTemplate(roomNode);
                if (roomTemplate == null) continue;

                //创建用于绘制的房间对象
                var room = CreateRoom(roomNode, roomTemplate);
                HandleRoom(room);
                roomList.Add(room);
            }

            //生成房间
            InitializeRoom();
        }

        /// <summary>
        /// 房间对象处理
        /// </summary>
        /// <param name="room"></param>
        private void HandleRoom(Room room)
        {
            if (!string.IsNullOrWhiteSpace(room.parentNodeId))
            {
                //正常房间
                var parentRoom = roomList.FirstOrDefault(p => p.id == room.parentNodeId);
                if (parentRoom == null)
                {
                    Debug.Log($"{room.id}未获取到房间对象");
                    return;
                }

                var parentDoorway = MatchParentDoorWay(parentRoom, room);
                if (parentDoorway == null)
                {
                    Debug.Log($"{room.id}没有匹配的门洞");
                    return;
                }

                PlaceRoom(parentRoom, room, parentDoorway);
            }
            else
            {
                //入口
                PlaceRoom(room);
            }
        }

        /// <summary>
        /// 放置房间位置
        /// </summary>
        private void PlaceRoom(Room parentRoom, Room room, RoomDoorway parentDoorway)
        {
            //找到与父房间相反方向的门洞来拼接
            var oppsiteDoor = GetOppositeDirection(parentDoorway.direction);
            var doorway = room.doorList.FirstOrDefault(p => !p.isConnected && p.direction == oppsiteDoor);
            if (doorway == null) return;

            //门洞的世界坐标位置
            Vector2Int parentDoorwayPosition = parentRoom.lowerBound + parentDoorway.entrancePosition - parentRoom.templateLowerBounds;

            Vector2Int adjustment = Vector2Int.zero;

            //如果门洞向东，则父门洞向西，当前门洞在父门洞的左侧，需要向右边调整一个单位[即X-1]
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

            //基于门洞位置计算房间的最小与最大位置
            room.lowerBound = parentDoorwayPosition + adjustment + room.templateLowerBounds - doorway.entrancePosition;
            room.upperBound = room.lowerBound + room.templateUpperBounds - room.templateLowerBounds;

            //doorway.isConnected = true;
            //parentDoorway.isConnected = true;
        }

        /// <summary>
        /// 放置房间【入口】
        /// </summary>
        /// <param name="room"></param>
        private void PlaceRoom(Room room)
        {
            room.lowerBound = room.templateLowerBounds;
            room.upperBound = room.lowerBound + room.templateUpperBounds - room.templateLowerBounds;
        }

        /// <summary>
        /// 初始化房间
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

        #region 工具方法
        /// <summary>
        /// 找到地图入口
        /// </summary>
        /// <param name="canvas"></param>
        private RoomNodeSO FindEntrance(RoomGraphCanvas canvas)
        {
            var entrance = canvas.nodeList.FirstOrDefault(p => string.IsNullOrWhiteSpace(p.parentNodeId));
            return entrance;
        }

        /// <summary>
        /// 查询区域模板
        /// </summary>
        /// <param name="roomNode"></param>
        private RoomTemplateSO FindRoomTemplate(RoomNodeSO roomNode)
        {
            var roomTemplate = roomTemplateList.FirstOrDefault(p => p.roomNodeType == roomNode.selectedRoomNodeType);
            return roomTemplate;
        }

        /// <summary>
        /// 匹配
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
        /// 获取反方向
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
        /// 创建房间对象
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