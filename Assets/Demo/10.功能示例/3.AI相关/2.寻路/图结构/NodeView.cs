using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.AI.PathFind.GraphWay
{
    /// <summary>
    /// 节点视图（UI显示）
    /// </summary>
    public class NodeView : MonoBehaviour
    {
        [Range(0, 0.5f)]
        public float borderSize = 0.15f;//边框尺寸

        public void Init(Node node)
        {
            this.name = $"Node({node.xIndex},{node.yIndex})";
            this.transform.position = node.position;

            this.transform.localScale = new Vector3(1f - borderSize, 1f - borderSize, 1);

        }

        public void ChangeColor(Color color, GameObject go)
        {
            var sr = go.GetComponent<SpriteRenderer>();
            sr.color = color;
        }

        public void ChangeColor(Color color)
        {
            ChangeColor(color, this.gameObject);
        }
    }
}