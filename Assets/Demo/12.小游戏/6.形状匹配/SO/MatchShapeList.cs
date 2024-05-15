using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.MatchShapes
{
    [CreateAssetMenu(fileName = "ShapeList", menuName = "小游戏/匹配游戏/形状列表")]
    public class MatchShapeList : ScriptableObject
    {
        public MatchShape[] list;
    }
}