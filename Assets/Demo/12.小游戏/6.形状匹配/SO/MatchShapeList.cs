using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.MatchShapes
{
    [CreateAssetMenu(fileName = "ShapeList", menuName = "形状匹配/形状列表")]
    public class MatchShapeList : ScriptableObject
    {
        public MatchShape[] list;
    }
}