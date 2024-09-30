using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.CustomCamera.MultiTarget
{
    public class PlayerMovement : MonoBehaviour
    {
        private void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            this.transform.Translate(new Vector2(x, y) * 5 * Time.deltaTime);
        }
    }
}
