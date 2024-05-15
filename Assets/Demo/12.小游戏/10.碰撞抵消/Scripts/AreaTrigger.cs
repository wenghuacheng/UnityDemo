using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.CollisionEliminate
{
    public class AreaTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.GetComponent<Player>();
            if (player != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    player.CreateFollower();
                }
                Destroy(this.gameObject);
            }
        }
    }
}