using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class GradientColorControoler : MonoBehaviour
    {
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image image;

        private float colorValue;

        void Start()
        {

        }

        void Update()
        {
            colorValue += Time.deltaTime;
            image.color = gradient.Evaluate(colorValue % 1);
        }
    }
}