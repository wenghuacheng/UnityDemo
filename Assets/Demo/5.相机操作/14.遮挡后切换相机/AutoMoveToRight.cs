using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera.ClearSlot
{
    public class AutoMoveToRight : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.transform.Translate(this.transform.right  * Time.deltaTime);
        }
    }
}