using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.InventorySysWithUI
{
    /// <summary>
    /// 魔法恢复类道具
    /// </summary>
    [CreateAssetMenu(fileName = "ItemManaPotion_", menuName = "带UI演示/库存系统/ItemManaPotion")]
    public class ItemManaPotion : InventoryItem
    {
        [Header("Config")]
        public float ManaValue;

        /// <summary>
        /// 物品使用
        /// </summary>
        /// <returns></returns>
        public override bool UseItem()
        {
            //PS:这边直接通过GameManager获取Player对象，可以考虑使用事件
            //调用PlayerMana中的CanRestoreMana判断是否可以恢复，再调用RestoreMana进行恢复
            return true;
        }
    }
}