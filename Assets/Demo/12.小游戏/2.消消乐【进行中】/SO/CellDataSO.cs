using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.XXL
{
    //[CreateAssetMenu(fileName = "CellData_", menuName = "消消乐/单元格数据")]
    public class CellDataSO : ScriptableObject
    {
        public int type;

        public GameObject prefab;
    }
}