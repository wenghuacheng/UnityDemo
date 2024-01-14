using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.InventorySysWithUI
{
    /// <summary>
    /// 生命恢复类道具
    /// </summary>
    [CreateAssetMenu(fileName = "ItemHealthPotion_", menuName = "带UI演示/库存系统/ItemHealthPotion")]
    public class ItemHealthPotion : InventoryItem
    {
        [Header("Config")]
        public float HealthValue;

        /// <summary>
        /// 物品使用
        /// </summary>
        /// <returns></returns>
        public override bool UseItem()
        {
            //PS:这边直接通过GameManager获取Player对象，可以考虑使用事件
            //调用PlayerHealth中的CanRestoreHealth判断是否可以恢复，再调用RestoreHealth进行恢复
            return true;
        }
    }
}