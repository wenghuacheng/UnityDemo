using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.CustomEditors.ToolKit
{
    public class NodeDataSO : ScriptableObject
    {
        public string guid;
        public Vector2 position;

        private void OnValidate()
        {
            if (guid != null)
            {
                guid = Guid.NewGuid().ToString();
            }
        }
    }
}
