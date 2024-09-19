using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Shaders
{
    public class FlashSpriteShaderTester : MonoBehaviour
    {
        private Shader shader;
        private SpriteRenderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();

        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 30), "flash"))
            {
                StopAllCoroutines();
                StartCoroutine(Flash());
            }
        }

        private IEnumerator Flash()
        {
            if (_renderer.material.HasProperty("_IsShowSolid"))
            {
                _renderer.material.SetInt("_IsShowSolid", 1);
                yield return new WaitForSeconds(0.3f);
                _renderer.material.SetInt("_IsShowSolid",0);
            }
        }
    }
}