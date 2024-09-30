using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomEditors
{
    public class TestHolder : MonoBehaviour
    {
        public TestEnumInfo test;

        private void Update()
        {
            Debug.Log(test.serializedPropertyValue);
        }
    }
}