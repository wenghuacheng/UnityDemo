using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.AssertStore.JoystickPackDemo
{
    public class Player : MonoBehaviour
    {
        //将JoystickPack中的prefab下的FixedJoystick拖入到Canvas中
        [SerializeField] private FixedJoystick joystick;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.transform.Translate(new Vector3(joystick.Horizontal, joystick.Vertical) * Time.deltaTime);
        }
    }
}