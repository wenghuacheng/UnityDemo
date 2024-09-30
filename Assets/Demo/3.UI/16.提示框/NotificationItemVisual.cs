using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI.Notifications
{
    public class NotificationItemVisual : MonoBehaviour
    {
        private void Awake()
        {
            Invoke("DestorySelf", 5f);
        }

        private void DestorySelf()
        {
            Destroy(this.gameObject);
        }
    }
}