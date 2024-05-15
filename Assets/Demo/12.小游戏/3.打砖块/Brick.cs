using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.BrickBreaker
{
    /// <summary>
    /// ×©¿é
    /// </summary>
    public class Brick : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ball")
            {
                Destroy(this.gameObject);
            }
        }
    }
}