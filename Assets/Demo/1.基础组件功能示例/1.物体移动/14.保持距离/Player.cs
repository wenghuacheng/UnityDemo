using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.ObjectMove.KeepDistance
{
    public class Player : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            this.transform.Translate(new Vector3(x, y) * Time.deltaTime * 3);
        }
    }
}