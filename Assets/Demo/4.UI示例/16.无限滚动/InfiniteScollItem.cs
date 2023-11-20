using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.UI
{
    public class InfiniteScollItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void SetText(string value)
        {
            text.text = value;
        }

        public string GetText()
        {
            return text.text;
        }
    }
}