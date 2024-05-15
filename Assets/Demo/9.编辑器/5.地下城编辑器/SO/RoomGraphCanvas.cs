using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.DungeonEditor
{
#if UNITY_EDITOR
    [CreateAssetMenu(fileName = "Level_", menuName = "带UI演示/地下城/房间画布")]
    public class RoomGraphCanvas : ScriptableObject
    {
        //房间节点信息
        [HideInInspector] public List<RoomNodeSO> nodeList = new List<RoomNodeSO>();

        //连线开始位置
        [HideInInspector] public Vector2 lineStartPosition;
        //连线结束位置
        [HideInInspector] public Vector2 lineEndPosition;

        /// <summary>
        /// 添加节点
        /// </summary>
        public void AddNode(RoomNodeSO node)
        {
            nodeList.Add(node);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="node"></param>
        public void RemoveNode(RoomNodeSO node)
        {
            nodeList.Remove(node);
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoomNodeSO GetNode(string id)
        {
            return nodeList.FirstOrDefault(p => p.id == id);
        }

    }
#endif
}