using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Shaders
{
    public class DissolutionGraphTest : MonoBehaviour
    {
        private SpriteRenderer render;
        private float value;

        private float multiper = 1;

        void Start()
        {
            render = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (value > 1f || value < 0)
            {
                multiper *= -1;
            }

            value += Time.deltaTime * 0.1F * multiper;
            render.material.SetFloat("_Float", value);
        }
    }
}