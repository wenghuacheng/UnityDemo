using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.DungeonEditor
{
#if UNITY_EDITOR
    [CreateAssetMenu(fileName = "Level_", menuName = "��UI��ʾ/���³�/���仭��")]
    public class RoomGraphCanvas : ScriptableObject
    {
        //����ڵ���Ϣ
        [HideInInspector] public List<RoomNodeSO> nodeList = new List<RoomNodeSO>();

        //���߿�ʼλ��
        [HideInInspector] public Vector2 lineStartPosition;
        //���߽���λ��
        [HideInInspector] public Vector2 lineEndPosition;

        /// <summary>
        /// ��ӽڵ�
        /// </summary>
        public void AddNode(RoomNodeSO node)
        {
            nodeList.Add(node);
        }

        /// <summary>
        /// ɾ���ڵ�
        /// </summary>
        /// <param name="node"></param>
        public void RemoveNode(RoomNodeSO node)
        {
            nodeList.Remove(node);
        }

        /// <summary>
        /// ��ȡ�ڵ�
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