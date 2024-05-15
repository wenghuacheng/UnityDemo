using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "��UI��ʾ/���ϵͳ/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
    }
}