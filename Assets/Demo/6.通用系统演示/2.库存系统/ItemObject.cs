using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    public class ItemObject : MonoBehaviour
    {
        [SerializeField] private ItemData itemData;

        private void OnValidate()
        {
            GetComponent<SpriteRenderer>().sprite = itemData.icon;
            gameObject.name = itemData.itemName;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //通过触发器添加到库存中

            Destroy(gameObject);
        }
    }
}