using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.IMGUI
{
    public class AttributeTestHolder : MonoBehaviour
    {
        public Ingredient ingredient;

        [CustomRange(1, 10)]
        public float value;
    }
}