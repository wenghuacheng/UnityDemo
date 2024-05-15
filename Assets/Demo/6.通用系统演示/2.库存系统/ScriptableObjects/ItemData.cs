using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "带UI演示/库存系统/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
    }
}