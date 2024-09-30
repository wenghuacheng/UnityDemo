using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.UIToolkit
{
    [ExecuteAlways]
    public class PartyDataScreenUI : MonoBehaviour
    {
        [SerializeField] private UIDocument document;

        private VisualElement visualElement;

        private void Awake()
        {
            visualElement = document.rootVisualElement;
        }

        private void OnGUI()
        {
            //¿ØÖÆÃæ°åÏÔÊ¾/Òþ²Ø
            if (GUI.Button(new Rect(0, 0, 100, 30), "Show"))
            {
                visualElement.style.display = DisplayStyle.Flex;
            }
            if (GUI.Button(new Rect(0, 30, 100, 30), "Hidden"))
            {
                visualElement.style.display = DisplayStyle.None;
            }
        }
    }
}


