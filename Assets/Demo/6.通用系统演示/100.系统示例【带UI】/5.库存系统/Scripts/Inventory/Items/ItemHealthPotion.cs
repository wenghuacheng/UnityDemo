using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.InventorySysWithUI
{
    /// <summary>
    /// �����ָ������
    /// </summary>
    [CreateAssetMenu(fileName = "ItemHealthPotion_", menuName = "��UI��ʾ/���ϵͳ/ItemHealthPotion")]
    public class ItemHealthPotion : InventoryItem
    {
        [Header("Config")]
        public float HealthValue;

        /// <summary>
        /// ��Ʒʹ��
        /// </summary>
        /// <returns></returns>
        public override bool UseItem()
        {
            //PS:���ֱ��ͨ��GameManager��ȡPlayer���󣬿��Կ���ʹ���¼�
            //����PlayerHealth�е�CanRestoreHealth�ж��Ƿ���Իָ����ٵ���RestoreHealth���лָ�
            return true;
        }
    }
}