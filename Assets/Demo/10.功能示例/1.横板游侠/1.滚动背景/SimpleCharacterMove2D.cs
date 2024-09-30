using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Backgrounds
{
    public class SimpleCharacterMove2D : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float speed = 5;
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.position = new Vector3(this.transform.position.x - speed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                this.transform.position = new Vector3(this.transform.position.x + speed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
            }
        }
    }
}