using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
    }
}