using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.MatchShapes
{
    [CreateAssetMenu(fileName = "ShapeList", menuName = "С��Ϸ/ƥ����Ϸ/��״�б�")]
    public class MatchShapeList : ScriptableObject
    {
        public MatchShape[] list;
    }
}