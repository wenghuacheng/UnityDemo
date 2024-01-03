using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Demo.Games.DefendWall
{
    public class Enemy : MonoBehaviour
    {
        private float speed = 2;

        void Update()
        {
            this.transform.Translate(Vector2.down * Time.deltaTime * speed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Wall")
            {
                Destroy(this.gameObject);
            }
        }
    }
}