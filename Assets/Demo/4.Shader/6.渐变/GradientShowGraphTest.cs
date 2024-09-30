using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Demo.Shaders
{
    public class GradientShowGraphTest : MonoBehaviour
    {
        private SpriteRenderer render;
        [SerializeField] private float value = 0.01f;

        private float multiper = 1;

        private float coolTime = 0;

        void Start()
        {
            render = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (coolTime > 0)
            {
                coolTime -= Time.deltaTime;
                return;
            }

            if (value >= 1f)
            {
                multiper = -1;
                coolTime = 2;
            }
            else if (value <= 0f)
            {
                multiper = 1;
                coolTime = 2;
            }

            value += Time.deltaTime * 0.1F * multiper;
            render.material.SetFloat("_Progress", value);
        }
    }
}