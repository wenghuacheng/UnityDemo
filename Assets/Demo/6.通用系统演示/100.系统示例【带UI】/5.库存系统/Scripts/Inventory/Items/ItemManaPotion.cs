using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.InventorySysWithUI
{
    /// <summary>
    /// ħ���ָ������
    /// </summary>
    [CreateAssetMenu(fileName = "ItemManaPotion_", menuName = "��UI��ʾ/���ϵͳ/ItemManaPotion")]
    public class ItemManaPotion : InventoryItem
    {
        [Header("Config")]
        public float ManaValue;

        /// <summary>
        /// ��Ʒʹ��
        /// </summary>
        /// <returns></returns>
        public override bool UseItem()
        {
            //PS:���ֱ��ͨ��GameManager��ȡPlayer���󣬿��Կ���ʹ���¼�
            //����PlayerMana�е�CanRestoreMana�ж��Ƿ���Իָ����ٵ���RestoreMana���лָ�
            return true;
        }
    }
}