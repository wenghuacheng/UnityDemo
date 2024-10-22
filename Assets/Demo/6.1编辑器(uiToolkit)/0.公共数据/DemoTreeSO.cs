using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.CustomEditors.ToolKit
{
    [CreateAssetMenu(fileName = "TreeSO", menuName = "UIToolKit/测试SO/树结构")]
    public class DemoTreeSO : ScriptableObject
    {
        public DemoTreeNodeSO rootNode;
        public List<DemoTreeNodeSO> nodes = new List<DemoTreeNodeSO>();
    }
}
