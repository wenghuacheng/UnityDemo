using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.CustomEditors.ToolKit
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class DemoTreeNodeSO : ScriptableObject
    {
        public List<DemoTreeNodeSO> Children { get; set; } = new List<DemoTreeNodeSO>();
    }
}
